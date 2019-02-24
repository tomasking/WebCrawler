# WebCrawler

### Some assumptions / quirks

- Crawler very specific to companies domain eg filtering done on hardcoded urls.
- I've got no logging frameworks, just writing to console for now
- www.somecompany.com same as somecompany.com
- I'm assuming the page has rendered already, any links dynamically created won't currently be crawled
- I've got no retry logic on the HTTP call, any failures will just return an empty page (and write error to console)
- Basic exception handling - if anywhere throws an error apart from the HTTP call, the whole crawl will terminate
- If you have more than one thread, you can get a different site map as any new link found will be put under the first page that is crawled 
  so if it's on multiple pages then it depends when the different threads processes that page
- I wasn't sure how to display the site map so I went with crawling the pages and whenever I saw a new page for the first time it would next under that page. 
  Seeing that page again linked to on another page would be ignored. Maybe a better thing to do when crawling is to have a node for every page, then any
  child links in that page would just have a reference to that page node. You could then display your site map however you want. 
