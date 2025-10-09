using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;
using SelectList = Microsoft.AspNetCore.Mvc.Rendering.SelectList;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using CSGenio.business;
using CSGenio.framework;
using GenioMVC.Helpers;
using GenioMVC.Models.Exception;
using GenioMVC.Models.Navigation;
using Quidgest.Persistence.GenericQuery;

namespace GenioMVC.ViewModels
{
	public class Homp_ViewModel(UserContext userContext, bool nestedForm = false) : EmptyFormViewModel(userContext, nestedForm)
	{
		#region DatabaseFields used in title buttons



		#endregion

		#region Tab region


		#endregion

		#region Foreign Keys


		#endregion

		#region Fields for formulas



		#endregion

		#region ViewModel Homp ()

		protected override void InitLevels()
		{
			this.RoleToShow = CSGenio.framework.Role.ROLE_1;
			this.RoleToEdit = CSGenio.framework.Role.ROLE_1;
		}

		public override void LoadPartial(NameValueCollection qs, bool lazyLoad = false)
		{
// USE /[MANUAL MOV VIEWMODEL_LOADPARTIAL HOMP]/
		}

		#endregion

		#region Custom code

// USE /[MANUAL MOV VIEWMODEL_CUSTOM HOMP]/

		#endregion
	}
}
