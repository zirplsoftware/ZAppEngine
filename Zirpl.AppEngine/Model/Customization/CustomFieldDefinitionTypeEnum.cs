using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zirpl.AppEngine.Model.Customization
{
    public enum CustomFieldDefinitionTypeEnum : byte
    {
        Text = 1,
        MultilineText = 2,
        YesNoDropDown = 3,
        YesNoRadioButtons = 4,
        YesNoCheckbox = 5,
        DropDown = 6,
        CheckBoxes = 7,
        Number = 8,
        Integer = 9,
        Decimal = 10,
        Currency = 11,
        Date = 12,
        Time = 13,
        DateTime = 14
    }
}
