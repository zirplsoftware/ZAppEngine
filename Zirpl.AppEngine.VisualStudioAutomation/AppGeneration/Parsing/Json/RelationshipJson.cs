﻿using System;
using Zirpl.AppEngine.Model.Metadata;
using Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Model;

namespace Zirpl.AppEngine.VisualStudioAutomation.AppGeneration.Parsing.Json
{
    internal sealed partial class JsonTypes
    {
        public class RelationshipJson
        {
            //relationship properties
            //
            public RelationshipTypeEnum? Type { get; set; }
            public RelationshipDeletionBehaviorTypeEnum? DeletionBehavior { get; set; }
            public String To { get; set; }
            public String ToPropertyName { get; set; }
            public bool? ToProperyIsRequired { get; set; }
            public UniquenessTypeEnum? ToPropertyUniqueness { get; set; }
        }
    }
}
