using System;
using Blackbaud.AppFx.UIModeling.Core;

namespace AIAInteractionsUIModel
{

	public partial class AIAUpdateInteractionMetadataUIModel
	{

        private void UpdateSubCategoryEnabledRequired()
        {
            bool flag = _interactioncategoryid.HasValue() && _interactioncategoryid.Value != Guid.Empty;
            _interactionsubcategoryid.Enabled = flag;
            _interactionsubcategoryid.Required = flag;
        }

        private void _interactioncategoryid_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!this.Loading)
            {
                _interactionsubcategoryid.Value = Guid.Empty;
            }
            _interactionsubcategoryid.ResetDataSource();
            UpdateSubCategoryEnabledRequired();
        }

		private void AIAUpdateInteractionMetadataUIModel_Loaded(object sender, LoadedEventArgs e)
		{
            RECORDTYPEID.Value = Blackbaud.AppFx.Constituent.UIModel.GlobalChangeHelper.GetRecordTypeID("INTERACTION", GetRequestContext());
            UpdateSubCategoryEnabledRequired();
		}

#region "Event handlers"

		partial void OnCreated()
		{
			this.Loaded += AIAUpdateInteractionMetadataUIModel_Loaded;
            this._interactioncategoryid.ValueChanged += _interactioncategoryid_ValueChanged;
		}

#endregion

	}

}