﻿using Gym4you.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Gym4you.Helpers
{
    [HtmlTargetElement("calendar", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CalendarTagHelper : TagHelper
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public List<Event> Events { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.Attributes.Add("class", "calendar");
            output.Content.SetHtmlContent(GetHtml());
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        private string GetHtml()
        {
            var monthStart = new DateTime(Year, Month, 1);
            var events = Events?.GroupBy(e => e.Date);

            var html = new XDocument(
                new XElement("div",
                    new XAttribute("class", "container-fluid"),
                    new XElement("header",
                        new XElement("h4",
                            new XAttribute("class", "display-4 mb-2 text-center"),
                            monthStart.ToString("MMMM yyyy")
                        ),
                        new XElement("div",
                            new XAttribute("class", "row d-none d-lg-flex p-1 bg-dark text-white"),
                            Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().Select(d =>
                                new XElement("h5",
                                    new XAttribute("class", "col-lg p-1 text-center"),
                                    d.ToString()
                                )
                            )
                        )
                    ),
                    new XElement("div",
                        new XAttribute("class", "row border border-right-0 border-bottom-0"),
                        GetDatesHtml()
                    )
                )
            );

            return html.ToString();

            IEnumerable<XElement> GetDatesHtml()
            {
                var startDate = monthStart.AddDays(-(int)monthStart.DayOfWeek);
                var dates = Enumerable.Range(0, 42).Select(i => startDate.AddDays(i));

                foreach (var d in dates)
                {
                    if (d.DayOfWeek == DayOfWeek.Sunday && d != startDate)
                    {
                        yield return new XElement("div",
                            new XAttribute("class", "w-100"),
                            String.Empty
                        );
                    }

                    var mutedClasses = "d-none d-lg-inline-block bg-light text-muted";
                    yield return new XElement("div",
                        new XAttribute("class", $"day col-lg p-2 border border-left-0 border-top-0 text-truncate {(d.Month != monthStart.Month ? mutedClasses : null)}"),
                        new XElement("h5",
                            new XAttribute("class", "row align-items-center"),
                            new XElement("span",
                                new XAttribute("class", "date col-1"),
                                d.Day
                            ),
                            new XElement("small",
                                new XAttribute("class", "col d-lg-flex text-center text-muted"),
                                d.DayOfWeek.ToString()
                            ),
                            new XElement("span",
                                new XAttribute("class", "col-1"),
                                String.Empty
                            )
                        ),
                        GetEventHtml(d)
                    );
                }
            }

            IEnumerable<XElement> GetEventHtml(DateTime d)
            {
                var eventsList = events?.Where(e => e.Key.Day == d.Day && e.Key.Month == d.Month)?.SelectMany(e => e);
                List<XElement> xElements = new List<XElement>();

                if (eventsList.Any())
                {
                    foreach (var e in eventsList)
                    {
                        xElements.Add(new XElement("p", new XElement("button",
                                               new XAttribute("class", $"event d-block p-1 pl-2 pr-2 mb-1 rounded text-truncate small bg-dark text-white btn"),
                                               new XAttribute("title", e.Instructor.FirstName + e.Title ?? "empty"),
                                               new XAttribute("data-toggle", "modal"),
                                               new XAttribute("data-target", "#calendarDetails"),
                                                new XAttribute("data-amount", e.Amount),
                                                 new XAttribute("data-title", e.Title ?? "empty"),
                                                  new XAttribute("data-fullname", e.Instructor.FirstName + " " + e.Instructor.LastName),
                                                   new XAttribute("data-time", e.Date),
                                                    new XAttribute("data-eventid", e.Id),
                                                new XElement("span", new XAttribute("class", "eventDetail"),
                                               $"{ e.Date.TimeOfDay } { e.Title }"),
                                               new XElement("span", new XAttribute("class", "eventDetail"),
                                               $"{e.Instructor.FirstName} {e.Instructor.LastName}"))));
                    }

                    return xElements;

                }
                else
                {
                    return new[] {
                     new XElement("p",
                    new XAttribute("class", "d-lg-none"),
                    "No events") };

                }


            };
        }
    }
}

