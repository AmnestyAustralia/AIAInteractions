using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Blackbaud.AppFx.Server;
using Blackbaud.AppFx.UIModeling.Core;
using Blackbaud.AppFx.Constituent.UIModel;
using Microsoft.VisualBasic.CompilerServices;

namespace AIAInteractionsUIModel
{

    public partial class AIAAddInteractionAttributeUIModel
	{
        public enum AttributeDataType
        {
            Text,
            Number,
            DateType,
            Currency,
            BooleanType,
            CodeTable,
            ConstituentRecord,
            FuzzyDate,
            Time,
            Memo
        }

        private UIField GetValueFieldForAttributeDataType(AttributeDataType attributeDataType)
        {
            switch (attributeDataType)
            {
                case AttributeDataType.BooleanType: return BOOLEANVALUE;
                case AttributeDataType.CodeTable: return CODETABLEVALUE;
			    case AttributeDataType.ConstituentRecord: return CONSTITUENTIDVALUE;
			    case AttributeDataType.Currency: return MONEYVALUE;
			    case AttributeDataType.DateType: return DATEVALUE;
			    case AttributeDataType.FuzzyDate: return FUZZYDATEVALUE;
			    case AttributeDataType.Memo: return MEMOVALUE;
			    case AttributeDataType.Number: return NUMBERVALUE;
			    case AttributeDataType.Text: return STRINGVALUE;
			    case AttributeDataType.Time: return HOURMINUTEVALUE;
			    default: return null;
		    }
        }

        private static readonly Guid ATTRIBUTEDATALISTID = new Guid("15C44580-593B-494D-9B6F-940D6B87F9CE");

        private DataListLoadReply _attributesDataList;

        public DataListLoadReply AttributesDataList
        {
            get
            {
                if (_attributesDataList == null)
                {
                    DataListLoadRequest dataListLoadRequest = new DataListLoadRequest();
                    dataListLoadRequest.DataListID = ATTRIBUTEDATALISTID;
                    dataListLoadRequest.SecurityContext = GetRequestSecurityContext();
                    _attributesDataList = ServiceMethods.DataListLoad(dataListLoadRequest, GetRequestContext());
                }
                return _attributesDataList;
            }
        }

        private bool _isMulticurrencyOn;

        private void IsMulticurrencyOn()
        {
            if (new AppFxWebService(GetRequestContext()).ConditionSettingExists(new ConditionSettingExistsRequest { Name = "Multicurrency" }).Exists)
            {
                _isMulticurrencyOn = true;
            }
        }

        private UIField _currentValueField;

        private void HideValueFields()
        {
            STRINGVALUE.Visible = false;
            STRINGVALUE.Required = false;
            NUMBERVALUE.Visible = false;
            NUMBERVALUE.Required = false;
            DATEVALUE.Visible = false;
            DATEVALUE.Required = false;
            MONEYVALUE.Visible = false;
            MONEYVALUE.Required = false;
            CURRENCYID.Visible = false;
            CURRENCYID.Required = false;
            BOOLEANVALUE.Visible = false;
            BOOLEANVALUE.Required = false;
            CODETABLEVALUE.Visible = false;
            CODETABLEVALUE.Required = false;
            CONSTITUENTIDVALUE.Visible = false;
            CONSTITUENTIDVALUE.Required = false;
            FUZZYDATEVALUE.Visible = false;
            FUZZYDATEVALUE.Required = false;
            MEMOVALUE.Visible = false;
            MEMOVALUE.Required = false;
            MEMOVALUE.Enabled = true;
            HOURMINUTEVALUE.Visible = false;
            HOURMINUTEVALUE.Required = false;
        }

        private void UpdateVisibleValueField()
        {
            if (AttributesDataList == null || !ATTRIBUTECATEGORYID.HasValue())
            {
                return;
            }
            DataListResultRow[] rows = AttributesDataList.Rows;
            foreach (DataListResultRow dataListResultRow in rows)
            {
                if (Operators.CompareString(dataListResultRow.Values[0], ATTRIBUTECATEGORYID.Value.ToString(), TextCompare: false) != 0)
                {
                    continue;
                }
                HideValueFields();
                AttributeDataType attributeDataType = (AttributeDataType)Conversions.ToInteger(dataListResultRow.Values[5]);
                _currentValueField = GetValueFieldForAttributeDataType(attributeDataType);
                _currentValueField.Visible = true;
                _currentValueField.Required = true;
                switch (attributeDataType)
                {
                    case AttributeDataType.ConstituentRecord:
                        CONSTITUENTIDVALUE.SearchListId = new Guid(dataListResultRow.Values[6]);
                        break;
                    case AttributeDataType.CodeTable:
                        CODETABLEVALUE.CodeTableName = dataListResultRow.Values[9];
                        CODETABLEVALUE.ResetDataSource();
                        break;
                    case AttributeDataType.Currency:
                        if (_isMulticurrencyOn)
                        {
                            CURRENCYID.Visible = true;
                            CURRENCYID.Required = true;
                            CURRENCYID.ReloadDataSource(CURRENCYID.DataSource);
                        }
                        break;
                }
            }
        }

        private void _attributecategoryid_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            UpdateVisibleValueField();
        }

        private void AIAAddInteractionAttributeUIModel_Loaded(object sender, LoadedEventArgs e)
		{
            IsMulticurrencyOn();
            CODETABLEVALUE.CodeTableName = null;
            RECORDTYPEID.Value = GlobalChangeHelper.GetRecordTypeID("INTERACTION", GetRequestContext());
            if (CURRENCYID.Value == Guid.Empty)
            {
                CURRENCYID.Value = CurrencyHelper.GetOrganizationCurrencyId(GetRequestContext());
            }
            HideValueFields();
            if (ATTRIBUTECATEGORYID.HasValue())
            {
                UpdateVisibleValueField();
            }
            else
            {
                MEMOVALUE.Visible = true;
                MEMOVALUE.Enabled = false;
            }
            IDSETREGISTERID.UpdateDisplayText();
        }

        private void AIAAddInteractionAttributeUIModel_BeginValidate(object sender, BeginValidateEventArgs e)
        {
            if (DateTime.Compare(STARTDATE.Value, DateTime.MinValue) != 0 && DateTime.Compare(ENDDATE.Value, DateTime.MinValue) != 0 && DateTime.Compare(STARTDATE.Value, ENDDATE.Value) > 0)
            {
                e.Valid = false;
                e.InvalidFieldName = "ENDDATE";
                e.InvalidReason = "The start date value must be on or before the end date value.";
            }
        }

#region "Event handlers"

        partial void OnCreated()
		{
			this.Loaded += AIAAddInteractionAttributeUIModel_Loaded;
            this.BeginValidate += AIAAddInteractionAttributeUIModel_BeginValidate;
            this._attributecategoryid.ValueChanged += _attributecategoryid_ValueChanged;
		}

#endregion

	}

}