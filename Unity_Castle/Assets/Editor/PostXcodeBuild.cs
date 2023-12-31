//PostXcodeBuild.cs

using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Collections;
using System.Collections.Generic;
#if UNITY_IOS
using UnityEditor.iOS.Xcode;
#endif

public class PostXcodeBuild
{
#if UNITY_IOS
    private struct InfoplistInfo
    {
        public string key;
        public string value;
        public InfoplistInfo(string str1, string str2)
        {
            key = str1;
            value = str2;
        }
    };

    private struct LocalizationInfo
    {
        public string lang;
        public bool isdefault;
        public InfoplistInfo[] infoplist;
        public LocalizationInfo(string langstr, bool flg, InfoplistInfo[] info)
        {
            lang = langstr;
            infoplist = info;
            isdefault = flg;
        }
    };
    private struct CommonInfoPlistInfo
    {
        public InfoplistInfo[] infoplist;
        public CommonInfoPlistInfo(InfoplistInfo[] info)
        {
            infoplist = info;
        }
    };

    private static LocalizationInfo[] localizationInfo = {
       new LocalizationInfo("en", true, new InfoplistInfo[]
       {new InfoplistInfo("CFBundleDisplayName",            "Steal Scroll"),
        new InfoplistInfo("NSUserTrackingUsageDescription", "Please set to Allow to avoid displaying inappropriate advertisements."),
       }),
       new LocalizationInfo("ja", false, new InfoplistInfo[]
       {new InfoplistInfo("CFBundleDisplayName",            "巻物盗みたい"),
        new InfoplistInfo("NSUserTrackingUsageDescription", "不適切な広告の表示を避けるために”トラッキングを許可”に設定してください。"),
       }),
       new LocalizationInfo("zh", false, new InfoplistInfo[]
       {new InfoplistInfo("CFBundleDisplayName",            "偷走一个卷轴"),
        new InfoplistInfo("NSUserTrackingUsageDescription", "请设置允许以避免显示不适当的广告。"),
       }),
       new LocalizationInfo("ko", false, new InfoplistInfo[]
       {new InfoplistInfo("CFBundleDisplayName",            "두루마리"),
        new InfoplistInfo("NSUserTrackingUsageDescription", "부적절한 광고를 표시하지 않으려면 허용으로 설정하십시오."),
       })
   };

   
    private static string[] skadnetworkitems = new string[] { "22mmun2rn5.skadnetwork", "238da6jt44.skadnetwork", "24t9a8vw3c.skadnetwork", "24zw6aqk47.skadnetwork", "252b5q8x7y.skadnetwork", "275upjj5gd.skadnetwork", "294l99pt4k.skadnetwork", "2fnua5tdw4.skadnetwork", "2u9pt9hc89.skadnetwork", "32z4fx6l9h.skadnetwork", "3l6bd9hu43.skadnetwork", "3qcr597p9d.skadnetwork", "3qy4746246.skadnetwork", "3rd42ekr43.skadnetwork", "3sh42y64q3.skadnetwork", "424m5254lk.skadnetwork", "4468km3ulz.skadnetwork", "44jx6755aq.skadnetwork", "44n7hlldy6.skadnetwork", "47vhws6wlr.skadnetwork", "488r3q3dtq.skadnetwork", "4dzt52r2t5.skadnetwork", "4fzdc2evr5.skadnetwork", "4mn522wn87.skadnetwork", "4pfyvq9l8r.skadnetwork", "4w7y6s5ca2.skadnetwork", "523jb4fst2.skadnetwork", "52fl2v3hgk.skadnetwork", "54nzkqm89y.skadnetwork", "578prtvx9j.skadnetwork", "5a6flpkh64.skadnetwork", "5l3tpt7t6e.skadnetwork", "5lm9lj6jb7.skadnetwork", "5tjdwbrq8w.skadnetwork", "6964rsfnh4.skadnetwork", "6g9af3uyq4.skadnetwork", "6p4ks3rnbw.skadnetwork", "6v7lgmsu45.skadnetwork", "6xzpu9s2p8.skadnetwork", "737z793b9f.skadnetwork", "74b6s63p6l.skadnetwork", "7953jerfzd.skadnetwork", "79pbpufp6p.skadnetwork", "7fmhfwg9en.skadnetwork", "7rz58n8ntl.skadnetwork", "7ug5zh24hu.skadnetwork", "84993kbrcf.skadnetwork", "89z7zv988g.skadnetwork", "8c4e2ghe7u.skadnetwork", "8m87ys6875.skadnetwork", "8r8llnkz5a.skadnetwork", "8s468mfl3y.skadnetwork", "97r2b46745.skadnetwork", "9b89h5y424.skadnetwork", "9nlqeag3gk.skadnetwork", "9rd848q2bz.skadnetwork", "9t245vhmpl.skadnetwork", "9vvzujtq5s.skadnetwork", "9yg77x724h.skadnetwork", "a2p9lx4jpn.skadnetwork", "a7xqa6mtl2.skadnetwork", "a8cz6cu7e5.skadnetwork", "av6w8kgt66.skadnetwork", "b9bk5wbcq9.skadnetwork", "bxvub5ada5.skadnetwork", "c3frkrj4fj.skadnetwork", "c6k4g5qg8m.skadnetwork", "cg4yq2srnc.skadnetwork", "cj5566h2ga.skadnetwork", "cp8zw746q7.skadnetwork", "cs644xg564.skadnetwork", "cstr6suwn9.skadnetwork", "dbu4b84rxf.skadnetwork", "dkc879ngq3.skadnetwork", "dzg6xy7pwj.skadnetwork", "e5fvkxwrpn.skadnetwork", "ecpz2srf59.skadnetwork", "eh6m2bh4zr.skadnetwork", "ejvt5qm6ak.skadnetwork", "f38h382jlk.skadnetwork", "f73kdq92p3.skadnetwork", "f7s53z58qe.skadnetwork", "feyaarzu9v.skadnetwork", "g28c52eehv.skadnetwork", "g2y4y55b64.skadnetwork", "ggvn48r87g.skadnetwork", "glqzh8vgby.skadnetwork", "gta8lk7p23.skadnetwork", "gta9lk7p23.skadnetwork", "gvmwg8q7h5.skadnetwork", "hb56zgv37p.skadnetwork", "hdw39hrw9y.skadnetwork", "hs6bdukanm.skadnetwork", "k674qkevps.skadnetwork", "kbd757ywx3.skadnetwork", "kbmxgpxpgc.skadnetwork", "klf5c3l5u5.skadnetwork", "krvm3zuq6h.skadnetwork", "lr83yxwka7.skadnetwork", "ludvb6z3bs.skadnetwork", "m297p6643m.skadnetwork", "m5mvw97r93.skadnetwork", "m8dbw4sv7c.skadnetwork", "mj797d8u6f.skadnetwork", "mlmmfzh3r3.skadnetwork", "mls7yz5dvl.skadnetwork", "mp6xlyr22a.skadnetwork", "mtkv5xtk9e.skadnetwork", "n38lu8286q.skadnetwork", "n66cz3y3bx.skadnetwork", "n6fk4nfna4.skadnetwork", "n9x2a789qt.skadnetwork", "nzq8sh4pbs.skadnetwork", "p78axxw29g.skadnetwork", "ppxm28t8ap.skadnetwork", "prcb7njmu6.skadnetwork", "pu4na253f3.skadnetwork", "pwa73g5rt2.skadnetwork", "pwdxu55a5a.skadnetwork", "qqp299437r.skadnetwork", "qu637u8glc.skadnetwork", "r45fhb6rf7.skadnetwork", "rvh3l7un93.skadnetwork", "rx5hdcabgc.skadnetwork", "s39g8k73mm.skadnetwork", "s69wq72ugq.skadnetwork", "su67r6k2v3.skadnetwork", "t38b2kh725.skadnetwork", "tl55sbb4fm.skadnetwork", "u679fj5vs4.skadnetwork", "uw77j35x4d.skadnetwork", "v4nxqhlyqp.skadnetwork", "v72qych5uu.skadnetwork", "v79kvwwj4g.skadnetwork", "v9wttpbfk9.skadnetwork", "vcra2ehyfk.skadnetwork", "vutu7akeur.skadnetwork", "w9q455wk68.skadnetwork", "wg4vff78zm.skadnetwork", "wzmmz9fp6w.skadnetwork", "x2jnk7ly8j.skadnetwork", "x44k69ngh6.skadnetwork", "x5l83yy675.skadnetwork", "x8jxxk4ff5.skadnetwork", "x8uqf25wch.skadnetwork", "xy9t38ct57.skadnetwork", "y45688jllp.skadnetwork", "y5ghdn5j9k.skadnetwork", "yclnxrl5pm.skadnetwork", "ydx93a7ass.skadnetwork", "yrqqpx2mcb.skadnetwork", "z4gj7hsk7h.skadnetwork", "zmvfpc5aq8.skadnetwork", "zq492l623r.skadnetwork"};


    static void createInfoPlistString(string pjdirpath, LocalizationInfo localizationinfo)
    {
        string dirpath = Path.Combine(pjdirpath, "Unity-iPhone Tests");

        if (!Directory.Exists(Path.Combine(dirpath, string.Format("{0}.lproj", localizationinfo.lang))))
        {
            Directory.CreateDirectory(Path.Combine(dirpath, string.Format("{0}.lproj", localizationinfo.lang)));
        }
        string plistpath = Path.Combine(dirpath, string.Format("{0}.lproj/InfoPlist.strings", localizationinfo.lang));
        StreamWriter w = new StreamWriter(plistpath, false);
        foreach (InfoplistInfo info in localizationinfo.infoplist)
        {
            string convertedval = System.Text.Encoding.UTF8.GetString(
                System.Text.Encoding.Convert(
                    System.Text.Encoding.Unicode,
                    System.Text.Encoding.UTF8,
                    System.Text.Encoding.Unicode.GetBytes(info.value)
                    )
            );
            w.WriteLine(string.Format(info.key + " = \"{0}\";", convertedval));
        }
        w.Close();
    }

    static void addknownRegions(string pjdirpath, LocalizationInfo[] info)
    {
        string strtmp = "";
        string pjpath = PBXProject.GetPBXProjectPath(pjdirpath);

        foreach (LocalizationInfo infotmp in info)
        {
            strtmp += "\t\t" + infotmp.lang + ",\n";
        }
        strtmp += "\t\t);\n";

        StreamReader r = new StreamReader(pjpath);
        string prjstr = "";
        string linetmp = "";
        while (r.Peek() >= 0)
        {
            linetmp = r.ReadLine();
            if (linetmp.IndexOf("knownRegions") != -1)
            {
                prjstr += linetmp + "\n";
                prjstr += strtmp;
                while (true)
                {
                    linetmp = r.ReadLine();
                    if (linetmp.IndexOf(");") != -1)
                    {
                        break;
                    }
                }
            }
            else
            {
                prjstr += linetmp + "\n";
            }
        }
        r.Close();
        StreamWriter sw = new StreamWriter(pjpath, false);
        sw.Write(prjstr);
        sw.Close();
    }

    static void addLocalizationInfoPlist(string pjdirpath, LocalizationInfo[] info)
    {
        string plistPath = Path.Combine(pjdirpath, "Info.plist");
        PlistDocument plist = new PlistDocument();

        plist.ReadFromFile(plistPath);
        var array = plist.root.CreateArray("CFBundleLocalizations");
        foreach (LocalizationInfo infotmp in info)
        {
            array.AddString(infotmp.lang);
        }
        var rootDict = plist.root;
        foreach (LocalizationInfo infotmp in info)
        {
            if (infotmp.isdefault)
            {
                foreach (InfoplistInfo pinfo in infotmp.infoplist)
                {
                    string convertedval = System.Text.Encoding.UTF8.GetString(
                        System.Text.Encoding.Convert(
                            System.Text.Encoding.Unicode,
                            System.Text.Encoding.UTF8,
                            System.Text.Encoding.Unicode.GetBytes(pinfo.value)
                    ));
                    rootDict.SetString(pinfo.key, convertedval);
                }
            }
        }
        plist.WriteToFile(plistPath);
    }

    static void addSkAdNetworkItems(string pjdirpath, string[] skadvallist)
    {
        string plistPath = Path.Combine(pjdirpath, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        if (skadvallist != null)
        {
            var array = plist.root.CreateArray("SKAdNetworkItems");
            foreach (string value in skadvallist)
            {
                PlistElementDict dict = array.AddDict();
                dict.SetString("SKAdNetworkIdentifier", value);
            }
        }
        plist.WriteToFile(plistPath);
    }
    
    //Info.plistに追加
    static void addInfoPlist(string pjdirpath)
    {
        var plistPath = Path.Combine(pjdirpath, "Info.plist");
        var plist = new PlistDocument();
        plist.ReadFromFile(plistPath);
        var root = plist.root;
        root.SetBoolean("GADIsAdManagerApp", true);
        root.SetString("AppLovinSdkKey", "DSyG4d2vgVLIVPHX7m4P25tWLVOu_ah_FV-4JPPv7J1K3LtK-7rzccbqQq04CTAacv-Sufl31buqLAhEmhsQ8w");
        plist.WriteToFile(plistPath);
    }

    static void addAppTrackingTransparency(string pathToBuiltProject)
    {
        // PBXProjectクラスというのを用いてAppTrackingTransparency.frameworkを追加していきます（ステップ３）
        string pbxProjectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
        PBXProject pbxProject = new PBXProject();
        pbxProject.ReadFromFile(pbxProjectPath);
        string targetGuid = pbxProject.GetUnityFrameworkTargetGuid();
        pbxProject.AddFrameworkToProject(targetGuid, "AppTrackingTransparency.framework", true);
        pbxProject.AddFrameworkToProject(targetGuid, "AdSupport.framework", true);
        pbxProject.WriteToFile(pbxProjectPath);
    }

    [PostProcessBuild]
    public static void SetXcodePlist(BuildTarget buildTarget, string pathToBuiltProject)
    {
        if (buildTarget != BuildTarget.iOS)
        {
            return;
        }

        foreach (LocalizationInfo entry in localizationInfo)
        {
            /* add infoplist.string */
            createInfoPlistString(pathToBuiltProject, entry);
        }

        /* add knownregions to project */
        addknownRegions(pathToBuiltProject, localizationInfo);

        /* add localization to infoplist */
        addLocalizationInfoPlist(pathToBuiltProject, localizationInfo);

        /* add addSkAdNetworkItems */
        addSkAdNetworkItems(pathToBuiltProject, skadnetworkitems);

        /* add AppTrackingTransparency */
        addAppTrackingTransparency(pathToBuiltProject);
        
        addInfoPlist(pathToBuiltProject);
    }
#endif
}