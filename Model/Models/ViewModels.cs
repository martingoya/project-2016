using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Model.Models
{
    public partial class TypeEventView
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeEventView()
        {
            this.Event = new HashSet<Event>();
        }

        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Event { get; set; }
    }

    public partial class ImageView
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ImageView()
        {
            this.Event = new HashSet<Event>();
            this.Event1 = new HashSet<Event>();
        }

        public string Title { get; set; }
        public string ImagePath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Event1 { get; set; }
    }

    public partial class EventView
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventView()
        {
            this.Images = new HashSet<Image>();
        }
        [Required]
        public string Title { get; set; }
        [Required]
        public int TypeEventID { get; set; }

        private DateTime _createdOn = DateTime.MinValue;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Date
        {
            get
            {
                return (_createdOn == DateTime.MinValue) ? DateTime.Now : _createdOn;
            }
            set { _createdOn = value; }
        }
        public string Text { get; set; }
        public string Introduction { get; set; }
        public int CoverImageID { get; set; }
        public string Path { get; set; }
        public string VideoLink { get; set; }

        public virtual Image CoverImage { get; set; }
        public virtual TypeEvent TypeEvent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Image> Images { get; set; }
        public ICollection<HttpPostedFileBase> Files { get; set; }
        public HttpPostedFileBase CoverFile { get; set; }
        public List<Event> RelativeEvents { get; set; } 
        public string PreviousEvent { get; set; }
        public string NextEvent { get; set; }
    }
}