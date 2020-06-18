using Microsoft.VisualBasic;
using System;
using System.IO;
using UpgradeHelpers.Helpers;

namespace Project1
{
	public class Persist
	{


		private string iniPath = "";
		private string INIfilename = "";
		private string key = "";

		public Persist()
		{
			iniPath = Interaction.Environ("APPDATA") + "\\" + Definitions.AppName;
			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{
				if (FileSystem.GetAttr(iniPath) != FileAttribute.Directory)
				{
					Directory.CreateDirectory(iniPath);
				}
				INIfilename = iniPath + "\\EQMOD.ini";
				key = "[default]";
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}
		}

		public void Store()
		{
		}

		public void Retrieve()
		{

		}


		// write using the default key and path
		public void WriteIniValue(string PutVariable, string PutValue)
		{
			WriteIniValueEx(PutVariable, PutValue, key, INIfilename);
		}

		// read using the default key and path
		public string ReadIniValue(ref string Variable)
		{
			return ReadIniValueEx(ref Variable, key, INIfilename);
		}

		// write using any key and path
		public void DeleteSection(string IniKey, string filepath)
		{

			int NF1 = 0;
			int NF2 = 0;
			string filepath2 = "";
			bool skip = false;
			string temp1 = "";

			//UPGRADE_TODO: (1065) Error handling statement (On Error Goto) could not be converted. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1065
			UpgradeHelpers.Helpers.NotUpgradedHelper.NotifyNotUpgradedElement("On Error Goto Label (handleErr)");


			filepath2 = filepath.Substring(0, Math.Min(Strings.Len(filepath) - 4, filepath.Length)) + "_tmp.ini";

			NF1 = FileSystem.FreeFile();
			NF2 = FileSystem.FreeFile();

			FileSystem.FileClose(NF1);
			FileSystem.FileClose(NF2);
			FileSystem.FileOpen(NF1, filepath, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
			FileSystem.FileOpen(NF2, filepath2, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);

			skip = false;
			while (!FileSystem.EOF(NF1))
			{
				temp1 = FileSystem.LineInput(NF1);

				if (temp1.StartsWith("["))
				{
					if (temp1 == IniKey)
					{
						// found secion to skip
						skip = true;
					}
					else
					{
						skip = false;
					}
				}
				if (!skip)
				{
					// copy to tempfile
					FileSystem.PrintLine(NF2, temp1);
				}
			}

			FileSystem.FileClose(NF1);
			FileSystem.FileClose(NF2);

			//UPGRADE_TODO: (1069) Error handling statement (On Error Resume Next) was converted to a pattern that might have a different behavior. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1069
			try
			{
				File.Delete(filepath);
				FileSystem.Rename(filepath2, filepath);

				FileSystem.FileClose(NF1);
				FileSystem.FileClose(NF2);
				return;

				handleErr:
				FileSystem.FileClose(NF1);
				FileSystem.FileClose(NF2);
			}
			catch (Exception exc)
			{
				NotUpgradedHelper.NotifyNotUpgradedElement("Resume in On-Error-Resume-Next Block");
			}

		}




		// write using any key and path
		public void WriteIniValueEx(string PutVariable, string PutValue, string IniKey, string filepath)
		{
			int HIKEY = 0;
			int VAR = 0;
			int VARENDOFLINE = 0;


			int NF = FileSystem.FreeFile();
			string ReadKey = Environment.NewLine + IniKey + "\r";
			int KEYLEN = Strings.Len(ReadKey);
			string ReadVariable = "\n" + PutVariable.ToLower() + "=";


			FileSystem.FileClose(NF);
			FileSystem.FileOpen(NF, filepath, OpenMode.Binary, OpenAccess.Default, OpenShare.Default, -1);
			FileSystem.FileClose(NF);
			(new FileInfo(filepath)).Attributes = FileAttributes.Archive;


			FileSystem.FileOpen(NF, filepath, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
			string temp = FileSystem.InputString(NF, (int) FileSystem.LOF(NF));
			temp = Environment.NewLine + temp + "[]";
			FileSystem.FileClose(NF);
			string LcaseTemp = temp.ToLower();


			int LOKEY = (LcaseTemp.IndexOf(ReadKey) + 1);
			if (LOKEY == 0)
			{
				goto AddKey;
			}
			HIKEY = Strings.InStr(LOKEY + KEYLEN, LcaseTemp, "[", CompareMethod.Binary);
			VAR = Strings.InStr(LOKEY, LcaseTemp, ReadVariable, CompareMethod.Binary);
			if (VAR > HIKEY || VAR < LOKEY)
			{
				goto AddVariable;
			}
			goto RenewVariable;

			AddKey:
			temp = temp.Substring(0, Math.Min(Strings.Len(temp) - 2, temp.Length));
			temp = temp + Environment.NewLine + Environment.NewLine + IniKey + Environment.NewLine + PutVariable + "=" + PutValue;
			goto TrimFinalString;

			AddVariable:
			temp = temp.Substring(0, Math.Min(Strings.Len(temp) - 2, temp.Length));
			temp = temp.Substring(0, Math.Min(LOKEY + KEYLEN, temp.Length)) + PutVariable + "=" + PutValue + Environment.NewLine + temp.Substring(LOKEY + KEYLEN);
			goto TrimFinalString;

			RenewVariable:
			temp = temp.Substring(0, Math.Min(Strings.Len(temp) - 2, temp.Length));
			VARENDOFLINE = Strings.InStr(VAR, temp, "\r", CompareMethod.Binary);
			temp = temp.Substring(0, Math.Min(VAR, temp.Length)) + PutVariable + "=" + PutValue + temp.Substring(VARENDOFLINE - 1);
			goto TrimFinalString;

			TrimFinalString:
			temp = temp.Substring(1);

			while((temp.IndexOf(Environment.NewLine + Environment.NewLine + Environment.NewLine) + 1) != 0)
			{
				temp = StringsHelper.Replace(temp, Environment.NewLine + Environment.NewLine + Environment.NewLine, Environment.NewLine + Environment.NewLine, 1, -1, CompareMethod.Binary);
			};


			while(String.CompareOrdinal(temp.Substring(Math.Max(temp.Length - 1, 0)), "\r") <= 0)
			{
				temp = temp.Substring(0, Math.Min(Strings.Len(temp) - 1, temp.Length));
			};


			while(String.CompareOrdinal(temp.Substring(0, Math.Min(1, temp.Length)), "\r") <= 0)
			{
				temp = temp.Substring(1);
			};


			FileSystem.FileOpen(NF, filepath, OpenMode.Output, OpenAccess.Default, OpenShare.Default, -1);
			FileSystem.PrintLine(NF, temp);
			FileSystem.FileClose(NF);

		}

		// read using any key and path
		public string ReadIniValueEx(ref string Variable, string IniKey, string filepath)
		{
			string result = "";
			string temp = "";
			string LcaseTemp = "";
			bool ReadyToRead = false;


			int NF = FileSystem.FreeFile();
			Variable = Variable.ToLower();


			FileSystem.FileClose(NF);
			FileSystem.FileOpen(NF, filepath, OpenMode.Binary, OpenAccess.Default, OpenShare.Default, -1);
			FileSystem.FileClose(NF);
			(new FileInfo(filepath)).Attributes = FileAttributes.Archive;


			FileSystem.FileOpen(NF, filepath, OpenMode.Input, OpenAccess.Default, OpenShare.Default, -1);
			while (!FileSystem.EOF(NF))
			{
				temp = FileSystem.LineInput(NF);
				LcaseTemp = temp.ToLower();
				if (LcaseTemp.IndexOf('[') >= 0)
				{
					ReadyToRead = false;
				}
				if (LcaseTemp == IniKey)
				{
					ReadyToRead = true;
				}
				if ((LcaseTemp.IndexOf('[') + 1) == 0 && ReadyToRead)
				{
					if ((LcaseTemp.IndexOf(Variable + "=") + 1) == 1)
					{
						result = temp.Substring(1 + Strings.Len(Variable + "=") - 1);
						FileSystem.FileClose(NF);
						return result;
					}
				}
			}
			FileSystem.FileClose(NF);
			// didn't find a value, look in the registry instead
			//UPGRADE_TODO: (1067) Member GetValue is not defined in type Profile. More Information: https://www.mobilize.net/vbtonet/ewis/ewi1067
			return Convert.ToString(Common.oProfile.GetValue(Common.oID, Variable));
		}

		public string GetIniPath()
		{
			return iniPath;
		}
	}
}