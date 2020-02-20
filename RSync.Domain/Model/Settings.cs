using RSync.Core.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSync.Domain.Model
{
    [Table(nameof(Settings))]
    public class Settings
    {
        [Key]
        public int SettingsId { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Column(nameof(AppLanguage), TypeName = "tinyint")]
        public AppLanguage AppLanguage { get; set; }

        public Settings(int userId, AppLanguage appLanguage)
        {
            UserId = userId;
            AppLanguage = appLanguage;
        }

        public virtual User User { get; set; }
    }
}
