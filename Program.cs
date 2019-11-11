/*
    fakesteam_launcher: Run an external application linked to a SteamAppID.

    Copyright (C) 2019  Lucas Cota (Brasileiro)
    lucasbrunocota@live.com
    <http://www.github.com/BrasileiroGamer/>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

/*
 *	Program.cs
 *	Author: Lucas Cota (Brasileiro)
 *	Description: FakeSteam Launcher.
 *  Date: 2019-11-10
 */

using System;
using System.IO;
using Steamworks;
using System.Diagnostics;

class FakeSteam {

    public static void Main(string[] args) {
        int appId = 0;
        string appPath = "";

        Console.WriteLine("FakeSteam Launcher v2019.1 by Lucas Cota (Brasileiro)\n");

        try {
            appId = int.Parse(args[0]);

            if (!File.Exists(args[1])) throw new FileNotFoundException();

            appPath = args[1];

        } catch (Exception) {

            Console.WriteLine("Usage: fakesteam.exe [appId: Integer] [appPath: String]");
            Console.WriteLine("Example: fakesteam.exe 480 hentai.exe");
            Environment.Exit(1);
        }

        Environment.SetEnvironmentVariable("SteamAppId", appId.ToString());

        Process game = new Process();

        game.StartInfo.FileName = appPath;
        game.StartInfo.WorkingDirectory = Path.GetDirectoryName(appPath);
        game.StartInfo.UseShellExecute = false;
        game.Start();

        game.WaitForInputIdle();

        if (!SteamAPI.Init()) {
            Console.WriteLine("[ERROR] SteamAPI initialization failed! Maybe your appId is not valid.");

            game.Kill();

            Environment.Exit(1);
        }

        game.WaitForExit();
    }
}
