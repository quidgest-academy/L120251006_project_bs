using ExecuteQueryCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using CSGenio.persistence;
using CSGenio.business;
using CSGenio.framework;
using Quidgest.Persistence.GenericQuery;
using Quidgest.Persistence;

namespace CSGenio.business
{
    public class ReindexFunctions
    {
        public PersistentSupport sp { get; set; }
        public User user { get; set; }
        public bool Zero { get; set; }

        public ReindexFunctions(PersistentSupport sp, User user, bool Zero = false) {
            this.sp = sp;
            this.user = user;
            this.Zero = Zero;
        }   

        public void DeleteInvalidRows(CancellationToken cToken) {
            List<int> zzstateToRemove = new List<int> { 1, 11 };
            DataMatrix dm;
            sp.openConnection();

            /* --- MOVMEM --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAmem.FldCodmem)
                .From(CSGenioAmem.AreaMEM)
                .Where(CriteriaSet.And().In(CSGenioAmem.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAmem model = new CSGenioAmem(user);
                model.ValCodmem = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- MOVMOVIES --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAmovie.FldCodmovie)
                .From(CSGenioAmovie.AreaMOVIE)
                .Where(CriteriaSet.And().In(CSGenioAmovie.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAmovie model = new CSGenioAmovie(user);
                model.ValCodmovie = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- UserLogin --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioApsw.FldCodpsw)
                .From(CSGenioApsw.AreaPSW)
                .Where(CriteriaSet.And().In(CSGenioApsw.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioApsw model = new CSGenioApsw(user);
                model.ValCodpsw = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- AsyncProcess --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAs_apr.FldCodascpr)
                .From(CSGenioAs_apr.AreaS_APR)
                .Where(CriteriaSet.And().In(CSGenioAs_apr.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAs_apr model = new CSGenioAs_apr(user);
                model.ValCodascpr = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- NotificationEmailSignature --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAs_nes.FldCodsigna)
                .From(CSGenioAs_nes.AreaS_NES)
                .Where(CriteriaSet.And().In(CSGenioAs_nes.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAs_nes model = new CSGenioAs_nes(user);
                model.ValCodsigna = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- NotificationMessage --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAs_nm.FldCodmesgs)
                .From(CSGenioAs_nm.AreaS_NM)
                .Where(CriteriaSet.And().In(CSGenioAs_nm.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAs_nm model = new CSGenioAs_nm(user);
                model.ValCodmesgs = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- MOVUSERPROFILE --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAuserp.FldCoduserp)
                .From(CSGenioAuserp.AreaUSERP)
                .Where(CriteriaSet.And().In(CSGenioAuserp.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAuserp model = new CSGenioAuserp(user);
                model.ValCoduserp = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- MOVCHARACTERS --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAchara.FldCodchara)
                .From(CSGenioAchara.AreaCHARA)
                .Where(CriteriaSet.And().In(CSGenioAchara.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAchara model = new CSGenioAchara(user);
                model.ValCodchara = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- MOVCOMMENTS --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAcomme.FldCodcomme)
                .From(CSGenioAcomme.AreaCOMME)
                .Where(CriteriaSet.And().In(CSGenioAcomme.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAcomme model = new CSGenioAcomme(user);
                model.ValCodcomme = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- MOVFAVORITES --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAfavor.FldCodfavor)
                .From(CSGenioAfavor.AreaFAVOR)
                .Where(CriteriaSet.And().In(CSGenioAfavor.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAfavor model = new CSGenioAfavor(user);
                model.ValCodfavor = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- MOVRATTING --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAratti.FldCodratti)
                .From(CSGenioAratti.AreaRATTI)
                .Where(CriteriaSet.And().In(CSGenioAratti.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAratti model = new CSGenioAratti(user);
                model.ValCodratti = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- AsyncProcessArgument --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAs_arg.FldCodargpr)
                .From(CSGenioAs_arg.AreaS_ARG)
                .Where(CriteriaSet.And().In(CSGenioAs_arg.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAs_arg model = new CSGenioAs_arg(user);
                model.ValCodargpr = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- AsyncProcessAttachments --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAs_pax.FldCodpranx)
                .From(CSGenioAs_pax.AreaS_PAX)
                .Where(CriteriaSet.And().In(CSGenioAs_pax.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAs_pax model = new CSGenioAs_pax(user);
                model.ValCodpranx = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                

            /* --- UserAuthorization --- */
            dm = sp.Execute(
                new SelectQuery()
                .Select(CSGenioAs_ua.FldCodua)
                .From(CSGenioAs_ua.AreaS_UA)
                .Where(CriteriaSet.And().In(CSGenioAs_ua.FldZzstate, zzstateToRemove))
                );

            for (int i = 0; i < dm.NumRows; i++)
            {
                CSGenioAs_ua model = new CSGenioAs_ua(user);
                model.ValCodua = dm.GetKey(i, 0);

                try
                {
                    model.delete(sp);
                }
                //Not every exception should be allowed to continue record deletion, only business exceptions need to be caught and allow to deletion continue.
                //If there are other types of exceptions, such as database connection problems, for example, execution should be stopped immediately
                catch(BusinessException ex)
                {
                    Log.Error((ex.UserMessage != null) ? ex.UserMessage : ex.Message);
                }
            }
                
            
            //Hard Coded Tabels
            //These can be directly removed

            /* --- MOVmem --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVmem")
                .Where(CriteriaSet.And().In("MOVmem", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVcfg --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVcfg")
                .Where(CriteriaSet.And().In("MOVcfg", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVlstusr --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVlstusr")
                .Where(CriteriaSet.And().In("MOVlstusr", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVlstcol --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVlstcol")
                .Where(CriteriaSet.And().In("MOVlstcol", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVlstren --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVlstren")
                .Where(CriteriaSet.And().In("MOVlstren", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVusrwid --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVusrwid")
                .Where(CriteriaSet.And().In("MOVusrwid", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVusrcfg --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVusrcfg")
                .Where(CriteriaSet.And().In("MOVusrcfg", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVusrset --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVusrset")
                .Where(CriteriaSet.And().In("MOVusrset", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVwkfact --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVwkfact")
                .Where(CriteriaSet.And().In("MOVwkfact", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVwkfcon --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVwkfcon")
                .Where(CriteriaSet.And().In("MOVwkfcon", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVwkflig --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVwkflig")
                .Where(CriteriaSet.And().In("MOVwkflig", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVwkflow --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVwkflow")
                .Where(CriteriaSet.And().In("MOVwkflow", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVnotifi --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVnotifi")
                .Where(CriteriaSet.And().In("MOVnotifi", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVprmfrm --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVprmfrm")
                .Where(CriteriaSet.And().In("MOVprmfrm", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVscrcrd --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVscrcrd")
                .Where(CriteriaSet.And().In("MOVscrcrd", "ZZSTATE", zzstateToRemove)));
                
            /* --- docums --- */
            sp.Execute(new DeleteQuery()
                .Delete("docums")
                .Where(CriteriaSet.And().In("docums", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVpostit --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVpostit")
                .Where(CriteriaSet.And().In("MOVpostit", "ZZSTATE", zzstateToRemove)));
                
            /* --- hashcd --- */
            sp.Execute(new DeleteQuery()
                .Delete("hashcd")
                .Where(CriteriaSet.And().In("hashcd", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOValerta --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOValerta")
                .Where(CriteriaSet.And().In("MOValerta", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOValtent --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOValtent")
                .Where(CriteriaSet.And().In("MOValtent", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVtalert --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVtalert")
                .Where(CriteriaSet.And().In("MOVtalert", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVdelega --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVdelega")
                .Where(CriteriaSet.And().In("MOVdelega", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVTABDINAMIC --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVTABDINAMIC")
                .Where(CriteriaSet.And().In("MOVTABDINAMIC", "ZZSTATE", zzstateToRemove)));
                
            /* --- UserAuthorization --- */
            sp.Execute(new DeleteQuery()
                .Delete("UserAuthorization")
                .Where(CriteriaSet.And().In("UserAuthorization", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOValtran --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOValtran")
                .Where(CriteriaSet.And().In("MOValtran", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVworkflowtask --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVworkflowtask")
                .Where(CriteriaSet.And().In("MOVworkflowtask", "ZZSTATE", zzstateToRemove)));
                
            /* --- MOVworkflowprocess --- */
            sp.Execute(new DeleteQuery()
                .Delete("MOVworkflowprocess")
                .Where(CriteriaSet.And().In("MOVworkflowprocess", "ZZSTATE", zzstateToRemove)));
                

            sp.closeConnection();
        }





        // USE /[MANUAL RDX_STEP]/
    }
}