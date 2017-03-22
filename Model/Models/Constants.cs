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

        public const string projectName = "Oh Lala Web";
        public const int elementsForView = 5;
        public const int relativesEventsForView = 3;

        public const string secretKeyReCaptcha = "6LdWdBkUAAAAAOmlC4NjdDoQLZnakI3ddZBvKYDM";

        public static string getFullPathImage(Event eventData, string fileName)
        {
            var path = "Content/Photos/" + eventData.TypeEvent.Name + "/" + fileName;
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