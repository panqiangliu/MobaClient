using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Text;
using System.IO;
using System;
using System.Data;
using Excel;

public class UIFactory
{
    //模板
    static string UIWindowMould = Application.dataPath + "/Editor Default Resources/uiwindow.txt";
    static string UIControl = Application.dataPath + "/Editor Default Resources/uicontrol.txt";
    static string ProtoPath = Application.dataPath + "/../../../Tools/xieyi/Proto配置表.xlsx";
        //@"J:\dibase\Tools\xieyi\Proto配置表2.xlsx";

    static string CSPath = Application.dataPath + "/../../../Tools/xieyi/Protoc_3.4.0_bin/cs";
    static string CSCopyPath = Application.dataPath + "/cs";

    //UI窗体资源路径
    static string ResPath = Application.dataPath + "/Resources/UIPrefab/";

    //ui预制件字典 key:名称 value:加载路径
    static Dictionary<string, string> UIPre = new Dictionary<string, string>();

    //gameobject字典 key:物体  value:物体中所有的按钮集合
    static Dictionary<GameObject, Button[]> GoList = new Dictionary<GameObject, Button[]>();

    static string onclick = "    public void xxxOnClick()" + "\n" + "    {" + "\n" + "\n" + "    }" + "\n";


    [MenuItem("UI工具/创建UIConfig")]
    public static void GetUIConfig()
    {
        StringBuilder uiEnum = new StringBuilder();//枚举成员
        StringBuilder uiDic = new StringBuilder();//字典添加的代码
       
        //获取到所有.prefab的文件
        List<FileInfo> fileList = FileCom.GetFile(ResPath, ".prefab");
        for (int i = 0; i < fileList.Count; i++)
        {
            //把路径里的/ 换成 \
            string res = (Application.dataPath + "/Resources/").Replace("/", "\\");// 
            uiEnum.Append("\n");//添加换行符
            uiEnum.Append("    " + fileList[i].Name.Replace(".prefab", "") + ",");

            //通过枚举访问 去掉.prefab的UI预制件名称 
            string key = "UIPrefab." + fileList[i].Name.Replace(".prefab", "");
            //加载的短路径
            string value = fileList[i].FullName.Replace(res, "").Replace(".prefab", "");
            //给字典添加内容的模板
            string dicTemplate = "        " + "uiDic.Add(key,@value);";
            uiDic.Append("\n");
            uiDic.Append(dicTemplate.Replace("key", key).Replace("value", "\"" + value + "\""));
        }

        Debug.Log(uiEnum);
        Debug.Log(uiDic);

        //读取到模板 替换模板内标记的内容 替换为我们上面uiEnum uiDic
        string configTemplatePath = Application.dataPath + "/Editor Default Resources/uiconfig.txt";

        var allText = FileCom.GetFileText(configTemplatePath);
        allText = allText.Replace("//枚举成员", uiEnum.ToString()).Replace("//配置初始化", uiDic.ToString());
        Debug.Log(allText);
        //写入的目标路径
        var destFile = Application.dataPath + "/Script/UIFrame/UIConfig.cs";
        //创建和写入代码
        FileCom.CreateFile(destFile, allText);
        //刷新的编辑器
        AssetDatabase.Refresh();
    }
    
    //根据预制件创建UIwindow脚本
    [MenuItem("UI工具/创建UIWindow")]
    public static void CreateUIModule()
    {
        //模板提供的代码 
        var UIWindowText = FileCom.GetFileText(UIWindowMould);
        
        //获取到所有.prefab的文件
        List<FileInfo> fileList = FileCom.GetFile(ResPath, ".prefab");

        //UIPre初始化:获取物体与加载路径
        for (int i = 0; i < fileList.Count; i++)
        {
            //把路径里的/ 换成 \
            string res = (Application.dataPath + "/Resources/").Replace("/", "\\");
            string key = fileList[i].Name.Replace(".prefab", "");
            //加载的短路径
            string value = fileList[i].FullName.Replace(res, "").Replace(".prefab", "");
            UIPre.Add(key, value);
        }

        //GoList初始化:获取物体与物体上的所有按钮
        foreach (var key in UIPre.Keys)
        {
            var go = Resources.Load(UIPre[key]) as GameObject;
            Debug.Log("物体:"+go.name);//预制件的名称
            Button[] btns = go.GetComponentsInChildren<Button>(true);//所有按钮
            
            GoList.Add(go, btns);
        }
        
        //创建每个物体对应的Window.cs文件
        foreach (var gKey in GoList.Keys)
        {
            string allText;
            var key = gKey;

            
            //承载变量声明的代码
            StringBuilder variable = new StringBuilder();
            //承载变量初始化的代码
            StringBuilder variableInit = new StringBuilder();

            //动态物体字典 dynamic 键:物体名称 值:加载路径
            Dictionary<string, string> dynDic = new Dictionary<string, string>();
            //动态物体初始化字典 键:物体名称 值:初始化的代码
            Dictionary<string, string> dynInitDic = new Dictionary<string, string>();

            //所有子物体 包括隐藏的
            Transform[] allChild = key.GetComponentsInChildren<Transform>(true);

            //对于动态物体的逻辑 "_dyn"
            for (int i = 0; i < allChild.Length; i++)
            {
                //如果名称包含"_dyn"表示是动态物体
                if (allChild[i].name.Contains("_dyn"))
                {
                    string loadPath = "";//加载路径
                    var c = allChild[i];//当前的子物体
                    while (c.parent != null)
                    {
                        loadPath = "/" + c.name + loadPath;
                        c = c.parent;
                    }

                    //加入动态物体管理的数据结构 key:物体名称 value:加载路径
                    if (dynDic.ContainsKey(allChild[i].name))
                    {
                        Debug.LogError("动态物体名称重复,请手动添加业务前缀:"+ allChild[i].name);
                        Debug.LogError("已有路径是:"+ dynDic[allChild[i].name]);
                        Debug.LogError("冲突路径是:" + loadPath.Remove(0, 1));
                        dynDic.Add(allChild[i].name, loadPath.Remove(0, 1));
                    }
                    

                    //每个child初始化的代码
                    StringBuilder cInit = new StringBuilder();
                    //这是声明的
                    variable.AppendLine(("    Transform variable;").Replace("variable", allChild[i].name));
                    //这是初始化的
                    cInit.AppendLine(("        variable=transform.Find(\"path\");")
                        .Replace("variable", allChild[i].name)
                        .Replace("path", loadPath.Remove(0, 1)));

                    //加入该物体初始化的代码
                    variableInit.Append(cInit);
                    //加入动态物体管理的字典
                    dynInitDic.Add(allChild[i].name, cInit.ToString());
                }
            }
           
            //用于承载条件分支的代码
            StringBuilder caseList = new StringBuilder();
            //用于承载条件分支的字典 key:判断语句 value:条件分支完整的代码
            Dictionary<string, string> caseDic = new Dictionary<string, string>();
            //用于承载方法名称
            List<string> func = new List<string>();

            //获取物体对应的按钮
            Button[] btns = GoList[key];

            //对每个按钮的逻辑
            for (int i = 0; i < btns.Length; i++)
            {
                string btn = btns[i].name;//按钮
                string fun = btn + "OnClick";//方法名称

                //把所有按钮都加入到存储条件分支代码的集合中
                if (!caseList.ToString().Contains(btn))
                {
                    //存储一个条件分支 完整的代码
                    StringBuilder caseTemp = new StringBuilder();
                    //case 片段是一定会替换的
                    string caseItem = ("                case \"**\":").Replace("**", btn);
                    caseTemp.AppendLine(caseItem);
                    caseTemp.AppendLine(("                    btns[i].onClick.AddListener(**);").Replace("**", fun));
                    caseTemp.AppendLine("                    break;");
                    //加入条件分支集合中
                    caseList.Append(caseTemp);
                    //加入到条件分支的字典中进行管理
                    caseDic.Add(caseItem, caseTemp.ToString());
                }

                //方法名称加入到列表里
                if (!func.Contains(fun))
                {
                    func.Add(fun);
                }
            }

            //判断预制件对应的xxxWindow脚本是否存在
            var destFile = Application.dataPath + "/Script/UIFrame/UIWindow/" + key.name + ".cs";
            if (FileCom.CheckFileExists(destFile))
            {
                //如果存在 就获取其中所有的代码 进行追加
                string scriptText = FileCom.GetFileText(destFile);

                //用于存储(动态物体)transform变量声明的代码
                StringBuilder traStr = new StringBuilder();
                //用于存储(动态物体)初始化的代码
                StringBuilder traInitStr = new StringBuilder();

                //用于存储条件分支的代码
                StringBuilder caseStr = new StringBuilder();
                //用于存储方法声明的代码
                StringBuilder funStr = new StringBuilder();

                //筛选新增的动态物体
                foreach (var dKey in dynDic.Keys)
                {
                    if (!scriptText.Contains(dynDic[dKey]))
                    {
                        traStr.Append(("    Transform variable;").Replace("variable", dKey));
                    }
                }
                //筛选新增的动态物体初始化
                foreach (var dKey in dynInitDic.Keys)
                {
                    if (!scriptText.Contains(dynInitDic[dKey]))
                    {
                        traInitStr.Append(dynInitDic[dKey]);
                    }
                }


                //筛选新增的条件分支
                foreach (var caseDickey in caseDic.Keys)
                {
                    if (!scriptText.Contains(caseDickey))
                    {
                        caseStr.Append(caseDic[caseDickey]);
                    }
                }

                //筛选新增的方法
                for (int i = 0; i < func.Count; i++)
                {
                    if (!scriptText.Contains(func[i]))
                    {
                        funStr.Append(("    public void xxxOnClick()" + "\n" + "    {" + "\n" + "\n" + "    }" + "\n")
                            .Replace("xxxOnClick", func[i]));
                    }
                }

                //在文中加入新增的变量
                if (traStr.ToString() != "")
                {
                    scriptText = scriptText.Replace("#region 全局变量", "#region 全局变量" + "\n" + traStr.ToString());
                }

                //在文中加入新增的初始化代码
                if (traInitStr.ToString() != "")
                {
                    scriptText = scriptText.Replace("#region 变量初始化", "#region 变量初始化" + "\n" + traInitStr.ToString());
                }

                //在文中加入新增的条件分支
                if (caseStr.ToString() != "")
                {
                    scriptText = scriptText.Replace("#region 注册按钮事件", "#region 注册按钮事件" + "\n" + caseStr.ToString());
                }

                //在文中加入新增的方法
                if (funStr.ToString() != "")
                {
                    Debug.Log("funstr:" + funStr);
                    scriptText = scriptText.Replace("#region 内部接口", "#region 内部接口"+"\n" + funStr.ToString());
                }

                //重新创建该脚本,写入新的代码,而非重写内容.
                //好处是当出现异常的时候,可以到回收站取回上个版本的代码
                FileCom.CreateFile(destFile, scriptText);
            }
            else
            {
                //如果uiwindow是未创建过的

                //存储所有方法的集合
                StringBuilder funList = new StringBuilder();

                //由每一个方法名称确定要生成的方法全文
                for (int i = 0; i < func.Count; i++)
                {
                    funList.Append(onclick.Replace("xxxOnClick", func[i]));
                }

                //从模板中获取和替换全局变量 变量初始化 条件分支 按钮事件
                allText = UIWindowText.Replace("classname", key.name)
                    .Replace("ControlName", key.name.Replace("Window","Control"))
                    .Replace("#region 全局变量", "#region 全局变量" + "\n"+ variable)
                    .Replace("#region 变量初始化", "#region 变量初始化" + "\n" + variableInit)
                    .Replace("#region 注册按钮事件", "#region 注册按钮事件" + "\n" + caseList.ToString())
                    .Replace("#region 内部接口", "#region 内部接口"+"\n" + funList.ToString());
                FileCom.CreateFile(destFile, allText);

            }
        }
        //刷新编辑器
        AssetDatabase.Refresh();
    }

    [MenuItem("UI工具/创建UI控制器(务必先更新UIWindow)")]
    public static void CreateUIControl()
    {
        //获取控制器的模板
        var controlText = FileCom.GetFileText(UIControl);

        //从protobuf配置表格中 获取数据 生成 脚本控制器 以及添加UIWindow中的对外的接口
        FileStream stream = File.Open(ProtoPath, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        //数据集,是excel的映射
        DataSet dataSet = excelReader.AsDataSet();

        //用于存储excel表的数据 key:表名 value:表的内容    
        Dictionary<string, List<ProtoData>> tabsDic = new Dictionary<string, List<ProtoData>>();
        
        //遍历每一张表
        for (int i = 0; i < dataSet.Tables.Count; i++)
        {
            //一张表中有多条协议 所以这里用List存储每条协议的数据
            //ProtoData是自己定义的类 用于存储每一条协议的
            List<ProtoData> tabList = new List<ProtoData>();
            //获取当前遍历到的表
            var tab = dataSet.Tables[i];
            //在unity工程里用到的库读取到的名称是不包含"$"的 Replace()留着也没影响
            //获取表名
            var TabName = tab.TableName.Replace("$", "").Split('_')[1];

            //遍历表中的每一行 
            for (int j = 1; j < tab.Rows.Count; j++)
            {
                var row = tab.Rows[j];

                //这里注意 行数索引是从1开始 但不是表的首行,而是第二行(实际的数据)
                //0协议id
                //1协议名称
                //2修饰符
                //3类型
                //4字段
                //5注释
                //如果协议ID存在
                if (row[1].ToString() != "")
                {
                    //单条协议对应的数据
                    ProtoData tabData = new ProtoData();

                    if (row[0].ToString() != "")
                    {
                        tabData.TabID = int.Parse(row[0].ToString());
                    }
                    //协议名称
                    if (row[1].ToString() != "")
                    {
                        tabData.ClassName = row[1].ToString();
                    }
                    if (row[5].ToString() != "")
                    {
                        tabData.Notes = row[5].ToString();
                    }

                    //root不是我们现在需要交互的数据 这片暂时忽略
                    if (TabName == "Root")
                    {
                        if (row[6].ToString() != "")
                        {
                            //"所属数据库"
                            tabData.DataBaseName = row[6].ToString();
                        }
                        //"生成数据表"
                        if (row[7].ToString() != "")
                        {
                            tabData.IsCreateTab = row[7].ToString() == "是";
                        }
                        //"生成存储过程"
                        if (row[8].ToString() != "")
                        {
                            tabData.IsCreateCMDFile = row[8].ToString() == "是";
                        }
                    }
                    //给协议容器加入该条协议
                    tabList.Add(tabData);
                }
                else
                {
                    //没有协议ID的行 则是声明该条协议用到的成员变量
                    if (row[4].ToString() != "")
                    {
                        string rowString = "";
                        //表示该字段是否为List类型的
                        string isList = row[2].ToString() == "repeated" ? "是" : "否";
                        //Root是存储基础数据的 而非游戏模块 这里的逻辑暂时忽略
                        if (TabName == "Root")
                        {
                            rowString = isList + "&" + row[3] + "&" + row[4]
                                + "&" + row[5] + "&" + row[9] + "&" + row[10]
                                + "&" + row[11];
                        }
                        else
                        {
                            //拼接每一行的数据 修饰符&类型&字段&注释
                            rowString = isList + "&" + row[3] + "&" + row[4]
                          + "&" + row[5];
                        }
                        //从容器中取出最后一条协议实体 给它的字段容器FieldList加入本行数据
                        tabList[tabList.Count - 1].FieldList.Add(rowString);
                    }
                }
            }
            //当一张表遍历结束的时候 就将相应的数据加入到字典中进行管理
            tabsDic.Add(TabName, tabList);
        }

        //取得每张表的数据 生成控制器 给window脚本加入外部接口
        foreach (var key in tabsDic.Keys)
        {
            string writeText = controlText;
            //存储监听来自网络事件的代码
            StringBuilder s2c = new StringBuilder();
            //存储发送数据给游戏服务器的接口
            StringBuilder c2s = new StringBuilder();

            //存储处理的接口
            StringBuilder handle = new StringBuilder();
            //存储窗体增加的外部接口
            StringBuilder winHandle = new StringBuilder();

            //存储所有待筛选的代码!
            //s2cdic 服务器->客户端 c2sdic客户端->服务器 
            //handledic 处理接口 winhandledic窗体的外部(处理)接口
            Dictionary<string, string> s2cdic = new Dictionary<string, string>();
            Dictionary<string, string> c2sdic = new Dictionary<string, string>();
            Dictionary<string, string> handledic = new Dictionary<string, string>();
            Dictionary<string, string> winhandledic = new Dictionary<string, string>();

            //如果这张表不是Root
            if (key != "Root")
            {
                //获取本表所有的协议
                List<ProtoData> protoDatas = tabsDic[key];
                //遍历每条协议
                for (int i = 0; i < protoDatas.Count; i++)
                {
                    Debug.Log("协议名称:"+protoDatas[i].ClassName);
                    //如果名称包含C2S表示 客户端要发送给服务器的协议 
                    if (protoDatas[i].ClassName.Contains("C2S"))
                    {
                        //那么需要生成 发送接口 的代码
                        StringBuilder c2sTemp = new StringBuilder();
                        c2sTemp.AppendLine(("    /// <summary> 注释 </summary>").Replace("注释", protoDatas[i].Notes));
                        c2sTemp.AppendLine(("    public void Send" + protoDatas[i].ClassName + "(参数 参数)").Replace("参数", protoDatas[i].ClassName));
                        c2sTemp.AppendLine("    {");
                        c2sTemp.AppendLine("");
                        c2sTemp.AppendLine("    }");
                        c2sTemp.AppendLine("");
                        c2s.Append(c2sTemp);
                        //加入字典 用于后续筛选
                        c2sdic.Add("Send" + protoDatas[i].ClassName, c2sTemp.ToString());
                    }

                    //如果是S2C 表示是服务器发送给客户端的代码
                    else if (protoDatas[i].ClassName.Contains("S2C"))
                    {
                        //那么就需要生成 添加监听 移除监听 以及监听绑定的方法 这些代码
                        StringBuilder s2cTemp = new StringBuilder();
                        //监听
                        s2cTemp.AppendLine(("        NetEvent.Instance.AddEventListener(id,parameter);").Replace("id", protoDatas[i].TabID.ToString()).Replace("parameter", "Handle" + protoDatas[i].ClassName));
                        s2c.Append(s2cTemp);
                        s2cdic.Add(s2cTemp.ToString(), s2cTemp.ToString());
                        //生成 窗体处理 网络事件的接口
                        StringBuilder winHandleTemp = new StringBuilder();
                        StringBuilder handleTemp = new StringBuilder();
                        //写方法 handlexxxs2c
                        handleTemp.AppendLine(("    /// <summary> 注释 </summary>").Replace("注释", protoDatas[i].Notes));
                        handleTemp.AppendLine(("    public void Handle" + protoDatas[i].ClassName + "(BufferEntity p)"));
                        handleTemp.AppendLine("    {");
                        handleTemp.AppendLine(("        classname s2cMsg = classname.Parser.ParseFrom(p.body);").Replace("classname", protoDatas[i].ClassName));
                        handleTemp.AppendLine(("        GetWindow().Handleclassname(s2cMsg);").Replace("classname", protoDatas[i].ClassName));
                        handleTemp.AppendLine("    }");
                        handleTemp.AppendLine("");

                        handle.Append(handleTemp);
                        //加入待筛选的字典中
                        handledic.Add("void Handle" + protoDatas[i].ClassName, handleTemp.ToString());

                        //UIWindow中的接口
                        winHandleTemp.AppendLine(("    /// <summary> 注释 </summary>").Replace("注释", protoDatas[i].Notes));
                        winHandleTemp.AppendLine(("    public void Handleclassname(classname p)").Replace("classname", protoDatas[i].ClassName));
                        winHandleTemp.AppendLine("    {");
                        winHandleTemp.AppendLine("");
                        winHandleTemp.AppendLine("    }");
                        winHandleTemp.AppendLine("");
                        winHandle.Append(winHandleTemp);
                        //加入待筛选的字典
                        winhandledic.Add("void Handle" + protoDatas[i].ClassName, winHandleTemp.ToString());
                    }
                }

                //获取对应的UIWindow脚本
                var winPath= Application.dataPath + "/Script/UIFrame/UIWindow/" + key + "Window.cs";
                Debug.Log(winPath);
                if (FileCom.CheckFileExists(winPath))
                {
                    //如果包含相应的窗体脚本 则进行筛选需要添加的代码
                    string context = FileCom.GetFileText(winPath);
                    //存储需要处理服务器消息的接口
                    StringBuilder s2cStr = new StringBuilder();
                    //遍历每一个处理事件
                    foreach (var wKey in winhandledic.Keys)
                    {
                        //判断现有的UIWindow中是否存在相应的内容 
                        if (!context.Contains(wKey))
                        {
                            //如果不存在 则表示需要新增代码进来
                            s2cStr.Append(winhandledic[wKey]);
                        }
                    }

                    //最终如果有需要新增的代码
                    if (s2cStr.ToString() != "")
                    {
                        //则进行更新到UIWindow全文中
                        context = context.Replace(@"#region 对外接口", "#region 对外接口" + "\n" + s2cStr.ToString());
                    }
                //重新创建XXXWindow.cs 
                //不直接更新的好处是:随时可以到回收站取回上个版本写的代码
                    FileCom.CreateFile(winPath, context);
                }


                //判断Control脚本是否存在
                var UIControl = Application.dataPath + "/Script/UIFrame/UIControl/" + key + "Control.cs";
                //如果存在
                if (FileCom.CheckFileExists(UIControl))
                {

                    string context = FileCom.GetFileText(UIControl);
                    //筛选新增的服务器发送给客户端的协议
                    StringBuilder s2cStr = new StringBuilder();
                    foreach (var s2cdickey in s2cdic.Keys)
                    {
                        //如果现有脚本中 不包含要添加的内容 就表示需要新增的
                        if (!context.Contains(s2cdickey))
                        {
                            s2cStr.Append(s2cdic[s2cdickey]);
                        }
                    }

                    //筛选客户端发送给服务器的接口 
                    StringBuilder c2sStr = new StringBuilder();
                    foreach (var c2sdickey in c2sdic.Keys)
                    {
                        if (!context.Contains(c2sdickey))
                        {
                            c2sStr.Append(c2sdic[c2sdickey]);
                        }
                    }

                    //筛选 处理协议的接口
                    StringBuilder handleStr = new StringBuilder();
                    foreach (var handledickey in handledic.Keys)
                    {
                        if (!context.Contains(handledickey))
                        {
                            handleStr.Append(handledic[handledickey]);
                        }
                    }

                    //新增 s2c的代码
                    if (s2cStr.ToString() != "")
                    {
                        context = context.Replace("#region 添加监听网络事件", "#region 添加监听网络事件" + "\n" + s2cStr.ToString())
                            .Replace("#region 移除监听网络事件", "#region 移除监听网络事件" + "\n"
                    + s2cStr.ToString().Replace("AddEventListener", "RemoveEventListener"));
                    }
                    if (handleStr.ToString() != "")
                    {
                        context = context.Replace(@"#region 处理网络协议", "#region 处理网络协议" + "\n" + handleStr);
                    }

                    if (c2sStr.ToString() != "")
                    {
                        context = context.Replace(@"#region 发送网络协议", "#region 发送网络协议" + "\n" + c2sStr);
                    }
                    //进行重新创建新的Control,将新的内容写入
                    FileCom.CreateFile(UIControl, context);
                }
                else
                {
                    //如果未存在UIControl
                    //则直接替换模板的数据
                    writeText = controlText.Replace("classname", key + "Control")
                        .Replace("WindowName", key+ "Window")
                        .Replace("#region 添加监听网络事件", "#region 添加监听网络事件" + "\n" + s2c.ToString())
                        .Replace("#region 移除监听网络事件", "#region 移除监听网络事件" + "\n"
                        + s2c.ToString().Replace("AddEventListener", "RemoveEventListener"))
                        .Replace(@"#region 处理网络协议", "#region 处理网络协议" + "\n" + handle)
                        .Replace(@"#region 发送网络协议", "#region 发送网络协议" + "\n" + c2s);
                    //直接创建UIControl
                    FileCom.CreateFile(UIControl, writeText);
                }

            }
        }
        //刷新的编辑器
        AssetDatabase.Refresh();
    }

    //前提依赖服务器重新生成->SVN更新到最新文件
    //这里只是将外部的文件替换到Unity工程中而已
    [MenuItem("UI工具/一键更新proto")]
    public static void UpdateProtoFile()
    {
        FileCom.DeleteFile(CSCopyPath);
        FileCom.CopyDirectory(CSPath, CSCopyPath);
        //刷新的编辑器
        AssetDatabase.Refresh();
    }

    [MenuItem("GameObject/添加Window后缀", priority = 49)]
    public static void UpdateNameAddWindow() {
 
        Transform[] transforms = Selection.transforms;
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].name.Contains("Window"))
            {
                Debug.Log(transforms[i].name + ":已存在Window字样");
            }
            else
            {
                transforms[i].gameObject.name = transforms[i].name + "Window";
            }
        }

    }

    [MenuItem("GameObject/添加_dyn(动态)后缀", priority = 49)]
    public static void UpdateNameAddDYN()
    {
        Transform[] transforms = Selection.transforms;
        for (int i = 0; i < transforms.Length; i++)
        {
            if (transforms[i].name.Contains("_dyn"))
            {
                Debug.Log(transforms[i].name + ":已存在_dyn字样");
            }
            else
            {
                transforms[i].gameObject.name = transforms[i].name + "_dyn";
            }
        }
    }

    [MenuItem("Assets/图片改成2D AND UI", priority = 49)]
    public static void SetTextureType()
    {
        //支持
        var t = Selection.objects;

        if (t.Length > 0)
        {

            for (int i = 0; i < t.Length; i++)
            {
             
                if (t[i] is  Texture2D)                 
                {
                   
                    string path = AssetDatabase.GetAssetPath(t[i]);//获取路径
                    //Debug.Log("0.0:" + path);
                    TextureImporter ti = TextureImporter.GetAtPath(path) as TextureImporter; //获取TextureImporter

                    ti.mipmapEnabled = false;

                    ti.spriteBorder = new Vector2(1920, 1080);

                    ti.textureType = TextureImporterType.Sprite;//设置为精灵模式

                    ti.SaveAndReimport(); //保存

                }

            }

        }
 
    }


    /*
    [MenuItem("Assets/创建物体:图片", priority = 49)]
    public static void CreateImage()
    {
        UnityEngine.Object[] objs = Selection.objects;
        Transform tra = GameObject.Find("UIRoot").transform;
        if (objs.Length > 0)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] is Texture2D)
                {
                    string path = AssetDatabase.GetAssetPath(objs[i]);//获取路径
                    TextureImporter ti = TextureImporter.GetAtPath(path) as TextureImporter; //获取TextureImporter
                    if (ti.textureType == TextureImporterType.Sprite)
                    {
                        //,typeof(Button)
                        GameObject go = new GameObject(objs[i].name,new Type[] {typeof(Image)});
                        //第二个参数 false 表示不修改空间信息
                        go.transform.SetParent(tra,false);
                        //var image = go.AddComponent<Image>();
                        var image = go.GetComponent<Image>();
                        image.type = Image.Type.Simple;
                        UnityEngine.Object newImg = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
                        Undo.RecordObject(image, "Change Image");//有了这句才可以用ctrl+z撤消此赋值操作
                        image.sprite = newImg as Sprite;
                        image.SetNativeSize();//设置它跟原图一样大小
                        //go.name = objs[i].name;
                    }
                }

            }
        }
    }



    [MenuItem("Assets/创建物体:图片和按钮", priority = 49)]
    public static void CreateImageHaveBtn()
    {
        UnityEngine.Object[] objs = Selection.objects;
        Transform tra = GameObject.Find("UIRoot").transform;
        if (objs.Length > 0)
        {
            for (int i = 0; i < objs.Length; i++)
            {
                if (objs[i] is Texture2D)
                {
                    string path = AssetDatabase.GetAssetPath(objs[i]);//获取路径
                    TextureImporter ti = TextureImporter.GetAtPath(path) as TextureImporter; //获取TextureImporter
                    if (ti.textureType == TextureImporterType.Sprite)
                    {
                        GameObject go = new GameObject(objs[i].name, new Type[] { typeof(Image), typeof(Button) });
                        go.transform.SetParent(tra, false);
                        //var image = go.AddComponent<Image>();
                        var image = go.GetComponent<Image>();
                        image.type = Image.Type.Simple;
                        UnityEngine.Object newImg = UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
                        Undo.RecordObject(image, "Change Image");//有了这句才可以用ctrl+z撤消此赋值操作
                        image.sprite = newImg as Sprite;
                        image.SetNativeSize();
                        go.name = objs[i].name;
                    }
                }

            }
        }

    }
        */
    [MenuItem("UI工具/open window")]
    public static void OpenEditor() {
        Debug.Log("sss");
        UIFactoryWin win = EditorWindow.GetWindow<UIFactoryWin>();
        win.titleContent = new GUIContent("UI扩展工具");
        win.Show();

        //Rect wr = new Rect(0, 0, 500, 500);
        //UIFactoryWin windows = (UIFactoryWin)EditorWindow.GetWindowWithRect(typeof(UIFactoryWin), wr, true, "UI扩展工具");
        //windows.Show(true);
    }


}




public class ProtoData
{
    public int TabID;
    public string ClassName = "";//协议名称
    public string DataBaseName = "";//属于哪个数据库的
    public bool IsCreateTab = false;//是否创建数据表
    public bool IsCreateCMDFile;//是否创建C#操作类
    public string Notes = "";
    public bool IsAutoAdd = false;

    //字段集合,格式:是否List,类型,名称,注释,默认值,是否主键,是否自增
    public List<string> FieldList = new List<string>();

    //public bool isAdd;//是否生成Control脚本
}

