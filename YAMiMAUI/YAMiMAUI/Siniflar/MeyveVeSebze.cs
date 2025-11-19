using System.ComponentModel.DataAnnotations;

namespace YAMiMAUI.Siniflar
{
    public class MeyveVeSebze
    {
        public Guid MId { get; set; }


        public string MAdi { get; set; }
        public Decimal MSatisF { get; set; }
        public Decimal MAlis { get; set; }
        public int MMiktari { get; set; }
    }
}
