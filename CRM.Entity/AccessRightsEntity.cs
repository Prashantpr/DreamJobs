using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Entity
{
   public class AccessRightsEntity
    {
       //  select em.ModuleId,emm.ModuleMenuId,ema.MenuActionId,ModuleName,MenuName,
       //ControllerName,ActionName,TaskName,
//emu.EmpId_N,emu.IsAuthenticate,emu.FreezStatus_N
        public string EmpId_N { get; set; }

        public int ModuleId { get; set; }
        public int ModuleMenuId { get; set; }
        public int MenuActionId { get; set; }
        public string ModuleName { get; set; }

        public string ModuleIcone { get; set; }
        public string MenuName { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }

        public int MenuOrder { get; set; }
        public string[] Branches { get; set; }
        public string[] Processes { get; set; }

        public string[] selectedBranches { get; set; }

        public string[] selectedProcesses { get; set; }

        public string[] selectedCities { get; set; }

        public int RoleId { get; set; }

        public string TaskName { get; set; }
      
        public int IsAuthenticate { get; set; }

        public bool IsAuthenticatebool { get; set; }

        public int FreezStatus_N { get; set; }

        public string xmlRoleAction { get; set; }

        public List<MenuId> listmenuId { get; set; }
    }

   public class MenuId
   {
       public List<List<string>> menuId { get; set; }
       public List<List<string>> menuActionId { get; set; }
   }

   public class EmpRole
   {
       public string RoleId { get; set; }

       public string RoleName { get; set; }
   }
}
