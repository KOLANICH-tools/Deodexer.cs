using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using SevenZip;

namespace Deodexer
{
	class Deodexer:IDisposable
	{
		Process java;
		Process zipalignProc;
		public const string smaliName = "smali.jar", baksmaliName = "baksmali.jar",
			defaultFrameworkDir = "framework", defaultClassesDex="classes.dex", defaultOutDir="out";
		string _frameworkDir,_classesDex,_outDir;

		public string frameworkDir{
			set{
				_frameworkDir = Path.GetFullPath(value);
			}
			get{
				return _frameworkDir;
			}
		}
		public string classesDex {
			set{
				_classesDex = Path.GetFullPath(value);
			}
			get{
				return _classesDex;
			}
		}
		public string outDir {
			set{
				_outDir = Path.GetFullPath(value);
				//_outDir = value;
			}
			get{
				return _outDir;
			}
		}

		private SevenZipCompressor compressor = new SevenZipCompressor();

		public Deodexer(){
			frameworkDir = defaultFrameworkDir;
			classesDex = defaultClassesDex;
			outDir = defaultOutDir;

			java = new Process();
			java.StartInfo.FileName = "java";
			java.StartInfo.UseShellExecute = false;
			compressor.ArchiveFormat=OutArchiveFormat.Zip;
			compressor.CompressionMode = CompressionMode.Append;

			zipalignProc = new Process();
			zipalignProc.StartInfo.FileName = "zipalign";
			zipalignProc.StartInfo.UseShellExecute = false;
		}

		public void backsmali(string fileName) {
			Console.WriteLine("Backsmaling " + fileName + " ...");
			java.StartInfo.Arguments = string.Format(CultureInfo.InvariantCulture,@"-jar ""{0}"" -d ""{1}"" -x ""{2}"" -o ""{3}""", baksmaliName, frameworkDir, fileName, outDir);
			java.Start();
			java.WaitForExit();
		}
		public void smali() {
			Console.WriteLine("Smaling...");
			java.StartInfo.Arguments = string.Format(CultureInfo.InvariantCulture,@"-jar ""{0}"" ""{1}"" -o ""{2}", smaliName, outDir, classesDex);
			java.Start();
			java.WaitForExit();
		}
		public void embedClassesDex(string fileName) {
			Console.WriteLine("Embedding classes.dex into  "+ fileName + " ...");
			compressor.CompressFiles(fileName, classesDex);
		}
		public void cleanup() {
			Console.WriteLine("Cleaning up");
			if (Directory.Exists(outDir)) deleteDir(outDir);
			if (Directory.Exists(classesDex)) Directory.Delete(classesDex);
		}

		static void deleteDir(string path){
			//http://stackoverflow.com/a/1703799/450946
			try
			{
				Directory.Delete(path, true);
			}
			catch (IOException){
				Thread.Sleep(1);
				Directory.Delete(path, true);
			}
			/*catch (UnauthorizedAccessException){
				//TODO: process this case
			}*/
		}


		public void deodex(string fileName){
			
			var odexFileName = fileName.Substring(0,fileName.LastIndexOf('.')+1)+"odex";
			if (!File.Exists(odexFileName))
			{
				Console.WriteLine("No .odex file for " + fileName+ " !");
				return;
			}
			cleanup();
			if (Directory.Exists(outDir)){
				throw new Exception("WTF??? out dir is not cleaned up... it is not safe to smali so it will put old garbage into our file");
			}
			backsmali(odexFileName);
			smali();
			if (!File.Exists(classesDex)) {
				Console.WriteLine("Unable to smali");
				return;
			}
			embedClassesDex(fileName);
			zipalign(fileName);
			File.Delete(odexFileName);
		}
		public void zipalign(string fileName){
			Console.WriteLine("Aligning ...");
			var fileNameAligned = fileName+Path.GetRandomFileName();
			zipalignProc.StartInfo.Arguments = string.Format(CultureInfo.InvariantCulture,@"-f 4 ""{0}"" ""{1}""", fileName, fileNameAligned);
			zipalignProc.Start();
			zipalignProc.WaitForExit();
			if (!File.Exists(fileNameAligned)){
				Console.WriteLine("Unable to align zip");
				return;
			}
			File.Delete(fileName);
			File.Move(fileNameAligned,fileName);
			Console.WriteLine("Zip succesfully aligned");
		}

		public void Dispose() {
			cleanup();
			java.Dispose();
			zipalignProc.Dispose();
		}
	}

}
