Deodexer
========
Used to [deodex](http://forum.xda-developers.com/showthread.php?t=2200349) apk and jar files for Android. Allows to deodex whole folder or some files.

Licensed under [Apache License](http://www.apache.org/licenses/LICENSE-2.0) [TL;DRLegal](http://tldrlegal.com/license/apache-license-2.0-%28apache-2.0%29). 

Installation
------------
-1 First of all you need Java and .Net.

0 Obtain this program (click Releases).

1 Obtain [newest smali and baksmali](https://code.google.com/p/smali/downloads/list) and put into the same folder.

2 Obtain [SevenZipSharp and 7z.dll](https://sevenzipsharp.codeplex.com/releases/) (or you can use my [fork](https://bitbucket.org/KOLANICH/sevenzipsharp), I had fixed some bugs in it).

3 Obtain [zipalign](https://developer.android.com/tools/help/zipalign.html) (it is included into [Android SDK](https://developer.android.com/sdk/index.html)).

4 Obtain [framework files](https://code.google.com/p/android-apktool/wiki/FrameworkFiles#Pulling_frameworks) and put them into the "framework" subfolder.


How to use
----------
0 Run deodexer. You will get deodexer shell.

1 Write "deodex". You will get a dialog where you should select files.
Also you can write something like "deodex fileName1.apk fileName2.jar ? fileName3.apk folder1path" : "?" will make it ask you about a file.

2 Wait.

3 Be happy ;)

Feel free to fork and contribute.