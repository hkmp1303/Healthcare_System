namespace HospitalApp;

using System;
using System.Collections.Generic;
using System.IO;



public class Duno
{
    public static string MemoryDir = "./file/";
    public static string MemoryUser = "user.txt";
    public static string UserSave = path.combine(MemoryDir, MemoryUser);

    public List<IUser> userlist = new();

    public CheckFile()
    {
        if (!Directory.exists)
        {
            Directory.CreateDirectory(MemoryDir);
        }
        if (!file.exists)
        {
            Directory.Createfile(MemoryUser);
        }
    }

    public void saveUSer()
    {

    }


}
