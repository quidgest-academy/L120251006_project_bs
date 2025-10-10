chmod +x ./AdminCLI;
echo QuidSpark!;
echo 'Running database configuration...';
./AdminCLI dbconfig-write -u sa -p QuidSpark! --server sqlserver --schema MOV0 --type SQLSERVER;
echo 'Reindexing database...';
./AdminCLI reindex -u sa -p QuidSpark!;


