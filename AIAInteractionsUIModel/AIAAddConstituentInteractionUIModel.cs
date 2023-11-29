using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Blackbaud.AppFx.UIModeling.Core;
using Blackbaud.AppFx.Products;
using Blackbaud.AppFx.Constituent.UIModel;

namespace AIAInteractionsUIModel
{

	public partial class AIAAddConstituentInteractionUIModel
	{

		private void AIAAddConstituentInteractionUIModel_Loaded(object sender, Blackbaud.AppFx.UIModeling.Core.LoadedEventArgs e)
		{
            RECORDTYPEID.Value = GlobalChangeHelper.GetRecordTypeID("CONSTITUENT", GetRequestContext());
            SITES.Visible = GetRequestContext().ProductIsInstalled(InstalledProducts.SiteSecurity);
            UpdateSubCategoryEnabledRequired();
            DisableActual();
            SetExpected();
        }

        private void SetActual()
        {
            ACTUALDATE.Enabled = !ACTUALDATERELATIVE.Value;
            ACTUALDATE.Required = !ACTUALDATERELATIVE.Value;
            ACTUALDATEDAYS.Enabled = ACTUALDATERELATIVE.Value;
            ACTUALDATEDAYS.Required = ACTUALDATERELATIVE.Value;
            if (ACTUALDATERELATIVE.Value == false)
            {
                ACTUALDATEDAYS.Value = 0;
            }
            else
            {
                ACTUALDATE.Value = DateTime.MinValue;
            }
        }

        private void DisableActual()
        {
            ACTUALDATE.Enabled = false;
            ACTUALDATE.Required = false;
            ACTUALDATE.Value = DateTime.MinValue;
            ACTUALDATERELATIVE.Enabled = false;
            ACTUALDATEDAYS.Enabled = false;
            ACTUALDATEDAYS.Required = false;
            ACTUALDATEDAYS.Value = 0;
        }

        private void SetExpected()
        {
            EXPECTEDDATE.Enabled = !EXPECTEDDATERELATIVE.Value;
            EXPECTEDDATE.Required = !EXPECTEDDATERELATIVE.Value;
            EXPECTEDDATEDAYS.Enabled = EXPECTEDDATERELATIVE.Value;
            EXPECTEDDATEDAYS.Required = EXPECTEDDATERELATIVE.Value;
            if (EXPECTEDDATERELATIVE.Value == false)
            {
                EXPECTEDDATEDAYS.Value = 0;
            }
            else
            {
                EXPECTEDDATE.Value = DateTime.MinValue;
            }
        }

        private void _actualdaterelative_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SetActual();
        }

        private void _expecteddaterelative_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SetExpected();
        }

        private void _statuscode_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (STATUSCODE.Value == STATUSCODES.Completed)
            {
                ACTUALDATE.Enabled = true;
                ACTUALDATE.Required = true;
                ACTUALDATERELATIVE.Enabled = true;
            }
            else
            {
                DisableActual();
            }
        }

        private void _interactioncategoryid_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!base.Loading)
            {
                _interactionsubcategoryid.Value = Guid.Empty;
            }
            _interactionsubcategoryid.ResetDataSource();
            UpdateSubCategoryEnabledRequired();
        }

        private void UpdateSubCategoryEnabledRequired()
        {
            bool flag = _interactioncategoryid.HasValue() && _interactioncategoryid.Value != Guid.Empty;
            _interactionsubcategoryid.Enabled = flag;
            _interactionsubcategoryid.Required = flag;
        }

        #region "Event handlers"

        partial void OnCreated()
		{
			this.Loaded += AIAAddConstituentInteractionUIModel_Loaded;
            this._statuscode.PropertyChanged += _statuscode_PropertyChanged;
            this._interactioncategoryid.ValueChanged += _interactioncategoryid_ValueChanged;
            this._actualdaterelative.ValueChanged += _actualdaterelative_ValueChanged;
            this._expecteddaterelative.ValueChanged += _expecteddaterelative_ValueChanged;
		}

#endregion

	}

}