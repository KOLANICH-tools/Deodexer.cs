Deodexer
========
**We have moved to https://codeberg.org/KOLANICH-tools/Deodexer.cs, grab new versions there.**

Under the disguise of "better security" Micro$oft-owned GitHub has [discriminated users of 1FA passwords](https://github.blog/2023-03-09-raising-the-bar-for-software-security-github-2fa-begins-march-13/) while having commercial interest in success of [FIDO 1FA specifications](https://fidoalliance.org/specifications/download/) and [Windows Hello implementation](https://support.microsoft.com/en-us/windows/passkeys-in-windows-301c8944-5ea2-452b-9886-97e4d2ef4422) which [it promotes as a replacement for passwords](https://github.blog/2023-07-12-introducing-passwordless-authentication-on-github-com/). It will result in dire consequencies and is competely inacceptable, [read why](https://codeberg.org/KOLANICH/Fuck-GuanTEEnomo).

If you don't want to participate in harming yourself, it is recommended to follow the lead and migrate somewhere away of GitHub and Micro$oft. Here is [the list of alternatives and rationales to do it](https://github.com/orgs/community/discussions/49869). If they delete the discussion, there are certain well-known places where you can get a copy of it. [Read why you should also leave GitHub](https://codeberg.org/KOLANICH/Fuck-GuanTEEnomo).

---

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
