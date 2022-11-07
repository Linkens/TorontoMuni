using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{

    public class AssetFile
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int Building_ID { get; set; }
        [ForeignKey(nameof(Building_ID))]
        public Asset Asset { get; set; }
        public int File_ID { get; set; }
        [ForeignKey(nameof(File_ID))]
        public HostedFile File { get; set; }
        public FileTypeEnum Type { get; set; }
    }
}
