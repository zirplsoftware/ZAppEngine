using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zirpl.AppEngine.Model;
using Zirpl.AppEngine.Model.Customization;
using Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Customization.Catalog;

namespace Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog
{
    public partial class ProductReview  : EntityBase<int>, ICustomizable<ProductReview, ProductReviewCustomFieldValue, int>
    {
		public ProductReview()
		{
			this.CustomFieldValues = this.CustomFieldValues ?? new List<ProductReviewCustomFieldValue>();
		}
		
		public virtual Zirpl.Examples.CodeGeneration.VS2013.Commerce.Model.Catalog.DisplayProduct DisplayProduct { get; set; }
		public virtual int DisplayProductId { get; set; }
		public virtual string ReviewerName { get; set; }
		public virtual string ReviewerLocation { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual string Text { get; set; }
		public virtual int Stars { get; set; }

		#region CustomFields
		public virtual IList<ProductReviewCustomFieldValue> CustomFieldValues { get; set; }
        public virtual IList<ICustomFieldValue> GetCustomFieldValues()
        {
            return this.CustomFieldValues.OfType<ICustomFieldValue>().ToList();
        }	
		public virtual void SetCustomFieldValues(IList<ICustomFieldValue> list)
		{
		    this.CustomFieldValues = list.OfType<ProductReviewCustomFieldValue>().ToList();
		}
		#endregion
    }
}

