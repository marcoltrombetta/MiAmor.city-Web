using System;
using System.Collections.Generic;

namespace MiAmor.Core
{
    public static class EventExtensions
    {
        public static string[] ParseTags(this EventPost EventPost)
        {
            if (EventPost == null)
                throw new ArgumentNullException("EventPost");

            var parsedTags = new List<string>();
            if (!String.IsNullOrEmpty(EventPost.Tags))
            {
                string[] tags2 = EventPost.Tags.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string tag2 in tags2)
                {
                    var tmp = tag2.Trim();
                    if (!String.IsNullOrEmpty(tmp))
                        parsedTags.Add(tmp);
                }
            }
            return parsedTags.ToArray();
        }
    }
}
