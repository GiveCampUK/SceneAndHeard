using System.Collections.Generic;
using System.Linq;
using System.Net;
using EasyHttp.Http;

namespace SceneAndHeardFeedback.Models
{
    public class EventBriteLayer
    {
        public List<Event> GetEvents(string appKey, string userKey, int organiserId)
        {
            var eventWrappers = Get<EventsWrapper>(appKey, userKey, organiserId).Events;

            return eventWrappers.Select(eventWrapper => eventWrapper.Event).ToList();
        }

        private T Get<T>(string appKey, string userKey, int organiserId)
        {
            var http = new HttpClient
                           {
                               Request = { Accept = HttpContentTypes.ApplicationJson }
                           };

            var url =
                string.Format("https://www.eventbrite.com/json/organizer_list_events?app_key={0}&user_key={1}&id={2}",
                              appKey, userKey, organiserId);

            try
            {
                var response = http.Get(url).StaticBody<T>();
                return response;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
}