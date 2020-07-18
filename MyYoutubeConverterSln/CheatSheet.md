### Main Page

- `MainPage `has a `TabView `which contains many tabs
- Each tab handles a conversion and is bound to one `YoutubeConverter` object
- Default page of `TabViewItem` is Google-like: Search bar and button. User would paste YouTube URL into it
- When press search button, load waiting picture and try to download mp4

- If fail, notify error and go back to default page. If success, open `Mp3ViewerPage`
- `TabViewItem` may somehow go forward or backward to re-use `YoutubeConverter`

#### Url Search Page

- This page's used for inputting YouTube URL
- It will try to find and download video

#### Mp3ViewerPage

- This page's used for preparing before convert
- In here, user will define Name/Tittle, Artist/Performer, Album, Thumbnail cover, Begin/End time for mp3 which is relative to mp4
- Also where to save file 

### Dev Note

- If a class was implemented `IDisposable`, we should dispose it either directly or indirectly. To dispose of the type directly, call its [Dispose](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable.dispose?view=netcore-3.1) method in a `try`/`catch` block. To dispose of it indirectly, use a language construct such as `using`
- `VideoLibrary` has `WebClient`, `HttpRequest`,... inside their objects. Try to re-use them as much as possible
- Windows' **Groove Music** could display mp3 thumbnail properly but **VLC** could

