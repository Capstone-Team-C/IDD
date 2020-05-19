using Newtonsoft.Json;
using System.Collections.Generic;

namespace Appserver.FormSubmit
{
    public class MileageForm : AbstractFormObject
    {
        private List<MileageRowItem> miles = new List<MileageRowItem>();
        public MileageForm() { }

        [JsonProperty("mileagesheet")]
        [JsonConverter(typeof(MileageRowConverter))]
        internal List<MileageRowItem> Mileage { get => miles; set => miles = value; }
        public string totalMiles { get; set; }
        public void addMileRow(string date, string miles, string group, string purpose) =>
            this.Mileage.Add(new MileageRowItem(date, miles, group, purpose));

        protected override AbstractFormObject FromTextract(TextractDocument.TextractDocument doc)
        {
            throw new System.NotImplementedException();
        }
    }
}
