# WebCrawler

### Some assumptions / quirks

- Crawler very specific to Monzo domain - filtering done on hardcoded urls.
- No logging
- www.monzo.com same as monzo.com
- I'm assuming the page has rendered already, any links dynamically created won't currently be crawled
- I've got no retry logic on the HTTP call, any failures will just return an empty page (and write error to console)
- If you have more than one thread, you can get a different site map as any new link found will be put under the first page that is crawled so if it's on multiple pages then it depends when the different thread processes that page
