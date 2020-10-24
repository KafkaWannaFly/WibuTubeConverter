### Main Page

- `MainPage ` has a `TabView ` which contains many tabs
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
- In here, user will define Name/Tittle, Artist/Performer, Album, Thumbnail cover,...
- Also, choose where to save file 

### Dev Note

- If a class was implemented `IDisposable`, we should dispose it either directly or indirectly. To dispose of the type directly, call its [Dispose](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable.dispose?view=netcore-3.1) method in a `try`/`catch` block. To dispose of it indirectly, use a language construct such as `using`
- `VideoLibrary` has `WebClient`, `HttpRequest`,... inside their objects. Try to re-use them as much as possible
- Windows' **Groove Music** sometimes couldn’t display mp3 thumbnail properly but **VLC** could, vice versa
- If define `TabViewItem` without `Header`, its contents may stay on `Header` area instead of page
- `ResourceDictionary` can be called from code behind like a `Dictionary`. Syntax: `this.Resources`. If `ResourceDictionary` was defined in `App.xaml`, use `App.Current.Resources`
- `Page` should be navigated to from a `Frame` rather than being instantiated. Or else, that `Page.Frame` will be `null` and we can’t navigate to others pages
- When binary content of an image file is changed, `Image` object’s pointing to it wouldn’t update value even we have casted `OnPropertyChanged`
- Read and write file in UWP are so painful, they only allow operation on some specific folder
- Without `IStorageFile`/`IStorageFolder` API, we will mostly be `Access Denied`. One way to   solve is do reading/writing in UWP default folder and copy content to desired place later