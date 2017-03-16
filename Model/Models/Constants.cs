using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model.Models
{
    public static class Constants
    {
        public enum Events
        {
            Fifteen,
            Bodas,
            NewBorn
        }

        public static string getFullPathImage(Event eventData, string fileName)
        {
            var path = "Content/Fotos/" + eventData.TypeEvent.Name + "/" + eventData.ID + "/" + fileName;
            return path;
        }

        public static void setFullPathAllImages(Event eventData)
        {
            eventData.CoverImage.ImagePath = getFullPathImage(eventData, eventData.CoverImage.ImagePath);
            foreach (var images in eventData.Images)
            {
                images.ImagePath = getFullPathImage(eventData, images.ImagePath);
            }
        }

        public static void setFullPathAllImagesForAllEvents(IEnumerable<Event> eventsData)
        {
            foreach (var eventData in eventsData)
            {
                setFullPathAllImages(eventData);
            }
        }

        public static void setFullPathOnlyCoverImage(Event eventData)
        {
            eventData.CoverImage.ImagePath = getFullPathImage(eventData, eventData.CoverImage.ImagePath);
        }

        public static void setFullPathOnlyCoverImageForAllEvents(IEnumerable<Event> eventsData)
        {
            foreach (var eventData in eventsData)
            {
                setFullPathOnlyCoverImage(eventData);
            }
        }
    }
}