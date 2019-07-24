# Description

Mimer is a library that could detect a file mime type from its binary representation.

# Why did I wrote my custom solution

The only way to detect the true mime type is to use the [win api `FindMimeFromData` function](https://docs.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/platform-apis/ms775107(v%3Dvs.85)). It doesn't work for me, I don't know why. And win api is a windows-only item.

# How it works

The `MimeDetector` reads first and last 256 bytes from a file. It calls a file header and a file footer.

Then it comapres the header and the footer with [the mime type magic numbers table](https://en.wikipedia.org/wiki/List_of_file_signatures)

The algorithm doesn't work for sure, but it could be a good start point.

# Usage

```
Mimer.DetectedMimeType mime = Mimer.MimeDetector.Detect(@"C:\myfile");
Console.WriteLine(mime);

var stringMimeType = Mimer.MimeDetector.MimeToString(mime);
Console.WriteLine(stringMimeType);
```

Do not fully trust the `MimeToString` function: I wrote it for my case only and will check it later.

# How to contribute

* Fork the repo
* Push changes to a branch
* Create a pull request from forked repository into this one