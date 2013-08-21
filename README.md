translink-opiaproxy
===================

A caching web proxy and clients for the TransLink OPIA REST API service (Built using ASP.NET Web API).

TransLink has provided access to their journey-planning and other public-transport related information by means of their OPIA API. 
It provides information related to stops, trips, landmarks, and address-lookups, and lets you work out how to get from one place to another using Brisbane's public transport system. The API is exposed via an authenticated, SSL-encrypted REST-over-HTTP API. 

**OpiaProxy** takes the hard work out of using the OPIA API, and then it just gets out of your way, so that you can build your applications without worrying too much about the plumbing.

Normally you'd need your own set of login/password credentials from TransLink to hit this API directly, and you'll still need them **if you are planning on hosting your own proxy**, perhaps by forking your own copy of this one. 
Alternatively, you can use the Azure shared instance I've set up for it (see details below). But if traffic or requests become too heavy, I'll have to move it to a dedicated instance and start charging a few bucks a month for it. I'm an indie developer when not at my day job, and it doesn't always pay well, if at all :).

## Why would I use it?
- The short answer: Build mobile, web and desktop applications quickly and easily, backed by TransLink's journey-planning and public transport scheduling infrastructure.
- Quick and **easy** authenticated access to the OPIA API REST service. No handing over your OPIA authentication credentials to clients, or embedding them in applications.
- It's written using ASP.NET Web API, using JSON for data transfer, so information can be sent and received agnostically (you could hit it with an AJAX request, for example)
- It uses .NET's built-in model-binding to provide strongly-typed objects representing the data, both to and from the API. This allows you to use LINQ and IQueryable/IEnumerable to build and manage relationships between data objects, and derive new ones
- Data objects arrive serialised to JSON to begin with, and they exactly match the structure of the OPIA API (this is a proxy, after all). 
- Easily portable to Xamarin/MonoTouch, so it's incredibly easy to build native mobile applications (sample app to come)
- Built-in caching of requests and responses (using [Akavache](https://github.com/github/akavache) ), with configurable cache TTL. Easily swappable via IoC if you decide you want to use a different caching engine. Empirically testing the delivery of cached responses using this proxy is 4x-8x faster (on average) than querying the OPIA service for the same information repeatedly.
- Extensible - Add your own controllers and methods, make new objects, swap out the caching mechanism, mash up the public transport information
with other systems... 
- It's Open Source. Don't like something? Fork it, fix it. Make it better. Make it do what YOU want it to do. 
- If you have an improvement that you think will benefit everyone, by all means submit a pull request.
- Found a bug? Fix it and submit a pull request. It'll help if you can submit a failed/passed test as well, to add to the test suite in the solution.

## What are all the bits?
- `OPIA.API.Client` - This handles all the communications between the `OPIA.API.Proxy` and the TransLink OPIA API service. It does the authentication (which requires your OPIA API login credentials, if you don't have these, see below) and handles the SSL encryption, as well as making the requests, handling the responses, and doing the (de)serialisation and automatic model-binding in between.
 
    *The various REST-related clients in here will be updated very soon, so that they can be used to hit the `OPIA.API.Proxy` itself. This is so that it you can use them directly by your application (minus the TransLink auth tokens).*

- `OPIA.API.Client.Tests` - Unit/Integration tests to ensure that the requests and query parameters are generated correctly, and to exercise the actual `OPIA.API.Client` libraries against TransLink's OPIA API service.
- `OPIA.API.Contracts` - Common data objects and Request/Response objects for everything. The kind of think that will end up in a portable class library, eventually.
- `OPIA.API.Proxy` - The actual proxy engine, hosted within an ASP.NET Web API application. There's no reason this can't be modified to be self-hosted or hosted in something like OWIN for example. It's really very basic.
- `OPIA.API.Proxy.Tests` - Some integration tests that exercise the `OPIA.API.Proxy` Web API controller methods by actually hitting the running application. You'll very quickly be able to see the possibilities by at these tests.

## How do I get started?
- Have a look at the sample console application to see how it connects, makes a request, and manipulates the results. There's also some source code available as LinqPad script(s). See the **Samples\Scripts** folder. If you don't know what LinqPad is, you should try it (http://www.linqpad.net/). It's awesome. You can open the script(s) in a text editor tho.

    If using LinqPad, remember to add the `MS Web API Client` NuGet package, and a reference to the `OPIA.API.Contracts` assembly DLL.
- There's an instance of the `OPIA.API.Proxy` Web API service running on an **Azure shared instance** at http://playopia.azurewebsites.net for demo and basic testing purposes, which you can hit using the sample code (samples are configured to use it by default). 

    *Please bear in mind it is a minimally spec'd Azure instance, so it may be a little slow if no one's used it in a while, or if lots of people are busy using it. If there's enough interest, or it starts to buckle, I'll move it across to a dedicated instance. That would mean I'll probably have to start charging a couple of bucks a month, in that case, but we'll see how we go...*

Results between your client and this proxy are running over HTTP, and **are not encrypted** (or at least not yet, though communications between the proxy and TransLink's OPIA service are). This is just because I've not got around to making and putting an SSL certificate up there. Sometime soon tho! 

- As I build out improvements to the proxy, I'll be pushing those up, and it'll be restarting, so it may be out of action occasionally. If it's not responding, or 404, just wait a few minutes and try again.

- There'll be a simple Xamarin.Android application along shortly too, to show how easy it is to build a native application for Android (or devices which support MonoTouch e.g. iPhone, iPad).


## How does it work? Easy...
`Your app`  **>** `JSON` **>** `OPIA.API.Proxy` **>** `OPIA.API.Contracts` **>** `OPIA.API.Client` **>** TransLink

`Your app` **<** `JSON` **<** `OPIA.API.Proxy` **<** `OPIA.API.Contracts` **<** `OPIA.API.Client` **<** TransLink
 
 **OR (even easier - see sample apps)** 

`Your app` **>** `.Contracts` **>** `.Proxy` **>** `.Contracts` **>** `.Client` **>** TransLink


`Your app` **<** `.Contracts` **<** `.Proxy` **<** `.Contracts` **<** `.Client` **<** TransLink
 
## More details? Ok...
1. Your app does a JSON **POST** to the relevant `Proxy` controller and method, using one of the `IRequest` objects in the `OPIA.API.Contracts`. Doing a `POST` to what appears to be `GET` method may annoy the REST purists, but see the code comments about why it was done this way.
2. The proxy checks to see if it has a cached version of a response to your request already. If it does, and it's still valid within the cached object's TTL, it returns it directly (it doesn't redirect to a `GET`). 
3. If you're using the `Contracts` in your client, to bind the JSON response to the returned of object, then boom! you're done.
4. If there isn't a cached version of a response to your query, the proxy invokes the relevant `Client` to make the same request, on your behalf, to the TransLink OPIA API service.
5. If it gets a response object (i.e. it's not an error), it caches it, and then hands the `IResponse` object back to your app. 
6. If your app is using the `IResponse` objects in the `Contracts`, the response will be deserialised and bound to a model instance for you. And then boom! You're done.
7. You can then extract the info you were after, and if necessary use it to build and fire off another, different, request to get related information.

Have a look at the `OPIA.API.Proxy.Tests\ProxyTests\ProxyXXXControllerMethodsTests.cs` code to see the kinds of requests you can make, and the kind of information that comes back. 
There's also some LinqPad scripts in the Samples directory, which exercise Location-based controller methods from a running proxy. LinqPad has an awesome `blah.Dump()` feature which gives you a great visual representation of what the objects look like. 

## A simple example

```cs

/* Remember to add reference to NuGet Web Api Client package, and to the OPIA.API.Contracts assembly*/

private HttpClient SetupNewHttpClient()
{
		string baseUrl = "http://playopia.azurewebsites.net/api/";  // or via .AppSettings["proxyApiBaseUrl"];
		var client = new HttpClient { BaseAddress = new Uri(baseUrl) };
		
		// change this to "application/xml" if you're determined to do it old-school.
		client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
		
		// any SSL validation callbacks, auth or setting of tokens or cookies
		// etc. for your specific proxy implementation will probably need to go in here. 
		return client;
}

public void GetStopsNearby_LocationId_MustGetStopsNearbyLocationId()
{
		HttpClient client = SetupNewHttpClient(); // normally you'd do this once per class, not once per method.
		var requestEntity = new StopsNearbyRequest
		{
			LocationId = "AD:Anzac Rd, Eudlo", // LocationId would need to be retrieved via another API call,
			UseWalkingDistance = true,
			RadiusInMetres = 2000,
			MaxResults = 10,
		};

		var response = client.PostAsJsonAsync("location/getstopsnearby", requestEntity).Result;
		response.EnsureSuccessStatusCode();
		StopsNearbyResponse result = response.Content.ReadAsAsync<StopsNearbyResponse>().Result;
    if (result.NearbyStops.Any())
    {
      foreach(var stop in result.NearbyStops)
      {
        Console.WriteLine("Stop Id: {0} is {1}m away", stop.StopId, stop.Distance.DistanceM);
      }
    }
    Console.WriteLine("{0} GetStopsNearby Found: {1}", DateTime.Now.ToString("s"), result.NearbyStops.Count());
}
```

## Quick FAQ
- **What License has this been released under** - The Apache v2 license (see http://choosealicense.com/licenses/). There's no warranty or usability guarantee on this, express or implied. In a nutshell: use and change at your own risk. By forking and modifying this code, the responsibility for supporting anything you build on it rests soley on you. 
- **What sort of support can I expect from TransLink if I use this?** - At the moment there's **no arrangement** in place with TransLink to support this product. I am not affiliated with them in any way. There are some invite-only developer Google groups which they've set up, which I am a member of, so if you have a question, send an email to developers@translink.com.au and ask them to add you. I built this to help me with some other stuff I am doing, and thought it might be useful to formalise and share it with others.
- **I want to run my own proxy, or clients. How do I get my own set of login credentials for the OPIA API?** - I'd suggest sending an email to developers@translink.com.au and just asking them. They're a nice bunch of guys, always happy to help. There are some restrictions around using your credentials in a mobile app that may end up with thousands of users, but have a chat to them.
- **What sort of SLA does your Azure shared instance offer?** - None. Zero. Beyond Microsoft's standard SLA for a shared instance on their Azure platform, I can't guarantee anything about how responsive it will be, or how often it might be down. Right now (as of 2013/08/21) it's up and running and there's no reason for me to turn it off. I plan to just leave it up there until I have to take it down (which may be never).
- **Do you accept pull requests?** - Depends on the merits of what's been done and what problem it solves. Bug fixes, certainly, especially if they're accompanied by unit test(s) of some sort.
- **Why didn't you use $Awesome_Framework_du_jour?** - I built it using the tools I am familiar with, and that are familiar to most .NET and JavaScript developers. I'm trying to solve a problem, not see how easy it was to build something in the new shiny. You want to do this in Node or Ruby or Haskell, go ahead, knock yourself out :)
- **Where to from here?** - There's a few things I'd like to add and upgrade, especially with the new Web API 2 coming out soon. Proper OData management for one. And there's all sorts of interesting stuff we could be doing with the Attribute routing. I'll wait for the final release tho - I've been burned in the past by using -Pre frameworks :). Controller method pre- and post-processing, maybe moving the whole caching and logging to an AOP implementation... the list goes on. 
- **ZOMG TEH CODEZ MAKES MY EYES BLEED WTFLOL??!!!1!** - You can do better? Let's see it then! :). The beauty of FOSS is that it's free-as-in-speech, not free-as-in-beer. It's easy to sit and throw rocks at someone else's code. So rather show us what you've got instead. Make this thing better. Or build your own more awesome one. The onus is on the developer community to lift each other up, not tear each other down.
