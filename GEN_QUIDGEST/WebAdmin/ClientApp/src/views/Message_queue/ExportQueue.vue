<template>
    <div class="modal fade" ref="modalForm" id="export_queues" tabindex="-1" role="dialog" aria-labelledby="export_queue_Title" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="system_setup_core_Title">{{ Resources.EXPORTACAO_DE_MQ29750 }}</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" @click="close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <row>
                        <text-input v-model="Model.queue" :label="Resources.NOME_DA_QUEUE56594" :isReadOnly="true" :size="'xlarge'"></text-input>
                    </row>
                    <row>
                        <text-input v-model="Model.Qyear" :label="Resources.ANO33022" :isReadOnly="true" :size="'xlarge'"></text-input>
                    </row>

                    <hr />
                    <section>{{Resources.CONDICAO44011}}</section>
                    <row>
                        <q-select
                            v-model="conditionField"
                            item-value="Value"
                            item-label="Text"
                            :items="fields"
                            :label="Resources.CAMPO46284"
                            size="xlarge" />
                    </row>
                    <row>
                        <q-select
                            v-model="conditionOp"
                            item-value="Value"
                            item-label="Text"
                            :items="ops"
                            :label="Resources.OPERACAO29482"
                            size="xlarge" />
                    </row>
                    <row>
                        <text-input v-model="conditionValue" :label="Resources.VALOR32448" :size="'xlarge'"></text-input>
                    </row>
                    <p>{{Resources.MENSAGENS_A_SER_EXPO34711}}: {{count}}</p>

                    <row v-if="progressModel.Active">
                        <q-label :label="Resources.PROGRESSO52692" />
                        <div class="progress">
                            <div class="progress-bar progress-bar-striped progress-bar-animated" :style="{ width: progressModel.Count + '%' }">
                                {{ progressModel.Count }}%
                            </div>
                        </div>
                        <div>{{ progressModel.Message }}</div>
                    </row>
                </div>
                <div class="modal-footer">
                    <q-button
                        :label="Resources.CANCELAR49513"
                        @click="close" />
                    <q-button
                        ref="export"
                        variant="bold"
                        :label="Resources.EXPORTAR35632"
                        @click="exportQueue" />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
  // @ is an alias to /src
  import { reusableMixin } from '@/mixins/mainMixin';
  import { QUtils } from '@/utils/mainUtils';

    export default {
        name: 'message_queue_export_queue',
        mixins: [reusableMixin],
        emits: ['close'],
        props: {
            Model: {
                required: true
            },
            show: {
                required: true
            }
        },
        data: function () {
            var vm = this;
            return {
                progressModel: {
                    Active: false,
                    Message: '',
                    Count: 0
                },
                uniqueId: '',
                count: 0,
                fields: [],
                ops: ['=', '<', '>', '<=', '>=', '!=', 'IN'].map(function (x) { return { Value: x, Text: x }; }),
                conditionField: '',
                conditionOp: '=',
                conditionValue: '',
                timeoutId: null,
			};
        },
        methods: {
            testExportQueue: function () {
                var vm = this;
                if (!vm.Model.queue)
                    return;
                var p = {
                    queue: vm.Model.queue,
                    year: vm.Model.Qyear,
                    conditionField: vm.conditionField,
                    conditionOp: vm.conditionOp,
                    conditionValue: vm.conditionValue
                };

                QUtils.postData('MessageQueue', 'TestExportQueue', null, p, function (data) {
                    vm.count = data.count;
                    vm.fields = data.fields.map(function (x) { return { Value: x, Text: x }; });
                    vm.timeoutId = null;
                });
            },
            timerTestExportQueue: function () {
                var vm = this;
                if (vm.timeoutId) clearTimeout(vm.timeoutId);
                vm.timeoutId = setTimeout(vm.testExportQueue, 3000);
            },
            exportQueue: function () {
                var vm = this;
                var p = {
                    queue: vm.Model.queue,
                    year: vm.Model.Qyear,
                    conditionField: vm.conditionField,
                    conditionOp: vm.conditionOp,
                    conditionValue: vm.conditionValue,
                    id: vm.uniqueId
                };

                QUtils.postData('MessageQueue', 'ExportQueues', null, p, function (data) {
                    $.each(data, function (propName, value) { vm.progressModel[propName] = value; });
                    $(vm.$refs.export).prop('disabled', true);
                    vm.startMonitorProgress();
                });
            },
            startMonitorProgress: function () {
                // Init monitors
                var vm = this;
                vm.progressModel.Count = 0; vm.progressModel.Message = '', vm.progressModel.Active = true;

                var intervalId = setInterval(function () {
                    QUtils.postData('MessageQueue', 'Progress', null, { id: vm.uniqueId }, function (data) {
                        $.each(data, function (propName, value) { vm.progressModel[propName] = value; });
                        if (data.Completed) {
                            clearInterval(intervalId);
                            $(vm.$refs.export).prop('disabled', false);
                            vm.progressModel.Active = false;
                        }
                    });
                }, 100);

            },
            close: function () {
                this.$emit('close');
            },
            initForm: function () {
                if (this.show) { $(this.$refs.modalForm).modal('show'); }
                else { $(this.$refs.modalForm).modal('hide'); }
                this.testExportQueue();
			}
        },
        created: function () {
            this.uniqueId = QUtils.createGuid();
        },
        mounted: function () {
            this.initForm();
        },
        watch: {
            'show': 'initForm',
            'conditionField': 'timerTestExportQueue',
            'conditionOp': 'timerTestExportQueue',
            'conditionValue': 'timerTestExportQueue',
        }
    };
</script>
