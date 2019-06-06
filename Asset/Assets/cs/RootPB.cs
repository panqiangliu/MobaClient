// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: RootPB.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace ProtoMsg {

  #region Messages
  public sealed class UserInfo : pb::IMessage {
    private static readonly pb::MessageParser<UserInfo> _parser = new pb::MessageParser<UserInfo>(() => new UserInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UserInfo> Parser { get { return _parser; } }

    /// <summary>Field number for the "ID" field.</summary>
    public const int IDFieldNumber = 1;
    private int iD_;
    /// <summary>
    ///用户ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ID {
      get { return iD_; }
      set {
        iD_ = value;
      }
    }

    /// <summary>Field number for the "Account" field.</summary>
    public const int AccountFieldNumber = 2;
    private string account_ = "";
    /// <summary>
    ///帐号
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Account {
      get { return account_; }
      set {
        account_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Password" field.</summary>
    public const int PasswordFieldNumber = 3;
    private string password_ = "";
    /// <summary>
    ///密码
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Password {
      get { return password_; }
      set {
        password_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "WCID" field.</summary>
    public const int WCIDFieldNumber = 4;
    private string wCID_ = "";
    /// <summary>
    ///微信(wechat)ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string WCID {
      get { return wCID_; }
      set {
        wCID_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "WCHeadURL" field.</summary>
    public const int WCHeadURLFieldNumber = 5;
    private string wCHeadURL_ = "";
    /// <summary>
    ///头像URL链接
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string WCHeadURL {
      get { return wCHeadURL_; }
      set {
        wCHeadURL_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Sex" field.</summary>
    public const int SexFieldNumber = 6;
    private string sex_ = "";
    /// <summary>
    ///性别: 0男 1女 2未知  
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Sex {
      get { return sex_; }
      set {
        sex_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Province" field.</summary>
    public const int ProvinceFieldNumber = 7;
    private string province_ = "";
    /// <summary>
    ///省份
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Province {
      get { return province_; }
      set {
        province_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "City" field.</summary>
    public const int CityFieldNumber = 8;
    private string city_ = "";
    /// <summary>
    ///城市
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string City {
      get { return city_; }
      set {
        city_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "County" field.</summary>
    public const int CountyFieldNumber = 9;
    private string county_ = "";
    /// <summary>
    ///区县
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string County {
      get { return county_; }
      set {
        county_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "PIN" field.</summary>
    public const int PINFieldNumber = 10;
    private string pIN_ = "";
    /// <summary>
    ///身份证(可能带x)
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string PIN {
      get { return pIN_; }
      set {
        pIN_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Phone" field.</summary>
    public const int PhoneFieldNumber = 11;
    private long phone_;
    /// <summary>
    ///手机号
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Phone {
      get { return phone_; }
      set {
        phone_ = value;
      }
    }

    /// <summary>Field number for the "State" field.</summary>
    public const int StateFieldNumber = 12;
    private int state_;
    /// <summary>
    ///状态0正常 1违规封禁 2保护性冻结
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int State {
      get { return state_; }
      set {
        state_ = value;
      }
    }

    /// <summary>Field number for the "MyServer" field.</summary>
    public const int MyServerFieldNumber = 13;
    private string myServer_ = "";
    /// <summary>
    ///我登录过的服务器(id,id,id）
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string MyServer {
      get { return myServer_; }
      set {
        myServer_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "IsOnLine" field.</summary>
    public const int IsOnLineFieldNumber = 14;
    private bool isOnLine_;
    /// <summary>
    ///是否在线
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsOnLine {
      get { return isOnLine_; }
      set {
        isOnLine_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (ID != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ID);
      }
      if (Account.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Account);
      }
      if (Password.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Password);
      }
      if (WCID.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(WCID);
      }
      if (WCHeadURL.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(WCHeadURL);
      }
      if (Sex.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(Sex);
      }
      if (Province.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(Province);
      }
      if (City.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(City);
      }
      if (County.Length != 0) {
        output.WriteRawTag(74);
        output.WriteString(County);
      }
      if (PIN.Length != 0) {
        output.WriteRawTag(82);
        output.WriteString(PIN);
      }
      if (Phone != 0L) {
        output.WriteRawTag(88);
        output.WriteInt64(Phone);
      }
      if (State != 0) {
        output.WriteRawTag(96);
        output.WriteInt32(State);
      }
      if (MyServer.Length != 0) {
        output.WriteRawTag(106);
        output.WriteString(MyServer);
      }
      if (IsOnLine != false) {
        output.WriteRawTag(112);
        output.WriteBool(IsOnLine);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ID);
      }
      if (Account.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Account);
      }
      if (Password.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
      }
      if (WCID.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(WCID);
      }
      if (WCHeadURL.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(WCHeadURL);
      }
      if (Sex.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Sex);
      }
      if (Province.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Province);
      }
      if (City.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(City);
      }
      if (County.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(County);
      }
      if (PIN.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PIN);
      }
      if (Phone != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Phone);
      }
      if (State != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(State);
      }
      if (MyServer.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(MyServer);
      }
      if (IsOnLine != false) {
        size += 1 + 1;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            ID = input.ReadInt32();
            break;
          }
          case 18: {
            Account = input.ReadString();
            break;
          }
          case 26: {
            Password = input.ReadString();
            break;
          }
          case 34: {
            WCID = input.ReadString();
            break;
          }
          case 42: {
            WCHeadURL = input.ReadString();
            break;
          }
          case 50: {
            Sex = input.ReadString();
            break;
          }
          case 58: {
            Province = input.ReadString();
            break;
          }
          case 66: {
            City = input.ReadString();
            break;
          }
          case 74: {
            County = input.ReadString();
            break;
          }
          case 82: {
            PIN = input.ReadString();
            break;
          }
          case 88: {
            Phone = input.ReadInt64();
            break;
          }
          case 96: {
            State = input.ReadInt32();
            break;
          }
          case 106: {
            MyServer = input.ReadString();
            break;
          }
          case 112: {
            IsOnLine = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  public sealed class ServerInfo : pb::IMessage {
    private static readonly pb::MessageParser<ServerInfo> _parser = new pb::MessageParser<ServerInfo>(() => new ServerInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ServerInfo> Parser { get { return _parser; } }

    /// <summary>Field number for the "ID" field.</summary>
    public const int IDFieldNumber = 1;
    private int iD_;
    /// <summary>
    ///服务器ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ID {
      get { return iD_; }
      set {
        iD_ = value;
      }
    }

    /// <summary>Field number for the "Position" field.</summary>
    public const int PositionFieldNumber = 2;
    private int position_;
    /// <summary>
    ///地理位置，0华南
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Position {
      get { return position_; }
      set {
        position_ = value;
      }
    }

    /// <summary>Field number for the "Name" field.</summary>
    public const int NameFieldNumber = 3;
    private string name_ = "";
    /// <summary>
    ///服务器名称
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "State" field.</summary>
    public const int StateFieldNumber = 4;
    private int state_;
    /// <summary>
    ///状态:0未开放 1开放
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int State {
      get { return state_; }
      set {
        state_ = value;
      }
    }

    /// <summary>Field number for the "Press" field.</summary>
    public const int PressFieldNumber = 5;
    private int press_;
    /// <summary>
    ///荷载:0空闲 1良好 2爆满
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Press {
      get { return press_; }
      set {
        press_ = value;
      }
    }

    /// <summary>Field number for the "IP" field.</summary>
    public const int IPFieldNumber = 6;
    private string iP_ = "";
    /// <summary>
    ///服务器IP
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string IP {
      get { return iP_; }
      set {
        iP_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Port" field.</summary>
    public const int PortFieldNumber = 7;
    private int port_;
    /// <summary>
    ///服务器端口
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Port {
      get { return port_; }
      set {
        port_ = value;
      }
    }

    /// <summary>Field number for the "IsRecommend" field.</summary>
    public const int IsRecommendFieldNumber = 8;
    private bool isRecommend_;
    /// <summary>
    ///是否推荐
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool IsRecommend {
      get { return isRecommend_; }
      set {
        isRecommend_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (ID != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ID);
      }
      if (Position != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Position);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Name);
      }
      if (State != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(State);
      }
      if (Press != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(Press);
      }
      if (IP.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(IP);
      }
      if (Port != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(Port);
      }
      if (IsRecommend != false) {
        output.WriteRawTag(64);
        output.WriteBool(IsRecommend);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ID);
      }
      if (Position != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Position);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (State != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(State);
      }
      if (Press != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Press);
      }
      if (IP.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(IP);
      }
      if (Port != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Port);
      }
      if (IsRecommend != false) {
        size += 1 + 1;
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            ID = input.ReadInt32();
            break;
          }
          case 16: {
            Position = input.ReadInt32();
            break;
          }
          case 26: {
            Name = input.ReadString();
            break;
          }
          case 32: {
            State = input.ReadInt32();
            break;
          }
          case 40: {
            Press = input.ReadInt32();
            break;
          }
          case 50: {
            IP = input.ReadString();
            break;
          }
          case 56: {
            Port = input.ReadInt32();
            break;
          }
          case 64: {
            IsRecommend = input.ReadBool();
            break;
          }
        }
      }
    }

  }

  public sealed class NewsInfo : pb::IMessage {
    private static readonly pb::MessageParser<NewsInfo> _parser = new pb::MessageParser<NewsInfo>(() => new NewsInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<NewsInfo> Parser { get { return _parser; } }

    /// <summary>Field number for the "ID" field.</summary>
    public const int IDFieldNumber = 1;
    private int iD_;
    /// <summary>
    ///公告ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ID {
      get { return iD_; }
      set {
        iD_ = value;
      }
    }

    /// <summary>Field number for the "NewsType" field.</summary>
    public const int NewsTypeFieldNumber = 2;
    private int newsType_;
    /// <summary>
    ///公告类型
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int NewsType {
      get { return newsType_; }
      set {
        newsType_ = value;
      }
    }

    /// <summary>Field number for the "NewsTitle" field.</summary>
    public const int NewsTitleFieldNumber = 3;
    private string newsTitle_ = "";
    /// <summary>
    ///标题
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string NewsTitle {
      get { return newsTitle_; }
      set {
        newsTitle_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Contents" field.</summary>
    public const int ContentsFieldNumber = 4;
    private string contents_ = "";
    /// <summary>
    ///公告内容
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Contents {
      get { return contents_; }
      set {
        contents_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (ID != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ID);
      }
      if (NewsType != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(NewsType);
      }
      if (NewsTitle.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(NewsTitle);
      }
      if (Contents.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Contents);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ID);
      }
      if (NewsType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(NewsType);
      }
      if (NewsTitle.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(NewsTitle);
      }
      if (Contents.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Contents);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            ID = input.ReadInt32();
            break;
          }
          case 16: {
            NewsType = input.ReadInt32();
            break;
          }
          case 26: {
            NewsTitle = input.ReadString();
            break;
          }
          case 34: {
            Contents = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed class RolesInfo : pb::IMessage {
    private static readonly pb::MessageParser<RolesInfo> _parser = new pb::MessageParser<RolesInfo>(() => new RolesInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<RolesInfo> Parser { get { return _parser; } }

    /// <summary>Field number for the "ID" field.</summary>
    public const int IDFieldNumber = 1;
    private int iD_;
    /// <summary>
    ///用户ID-与账号关联
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ID {
      get { return iD_; }
      set {
        iD_ = value;
      }
    }

    /// <summary>Field number for the "RolesID" field.</summary>
    public const int RolesIDFieldNumber = 2;
    private int rolesID_;
    /// <summary>
    ///角色ID（自动生成）
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int RolesID {
      get { return rolesID_; }
      set {
        rolesID_ = value;
      }
    }

    /// <summary>Field number for the "GameCount" field.</summary>
    public const int GameCountFieldNumber = 3;
    private int gameCount_;
    /// <summary>
    ///游戏进行的场数（推算等级）
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GameCount {
      get { return gameCount_; }
      set {
        gameCount_ = value;
      }
    }

    /// <summary>Field number for the "State" field.</summary>
    public const int StateFieldNumber = 4;
    private int state_;
    /// <summary>
    ///状态：0休闲 1游戏中 2离线
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int State {
      get { return state_; }
      set {
        state_ = value;
      }
    }

    /// <summary>Field number for the "Integral" field.</summary>
    public const int IntegralFieldNumber = 5;
    private int integral_;
    /// <summary>
    ///角色积分
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Integral {
      get { return integral_; }
      set {
        integral_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (ID != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ID);
      }
      if (RolesID != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(RolesID);
      }
      if (GameCount != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(GameCount);
      }
      if (State != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(State);
      }
      if (Integral != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(Integral);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ID);
      }
      if (RolesID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RolesID);
      }
      if (GameCount != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(GameCount);
      }
      if (State != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(State);
      }
      if (Integral != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Integral);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            ID = input.ReadInt32();
            break;
          }
          case 16: {
            RolesID = input.ReadInt32();
            break;
          }
          case 24: {
            GameCount = input.ReadInt32();
            break;
          }
          case 32: {
            State = input.ReadInt32();
            break;
          }
          case 40: {
            Integral = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed class LinkmanInfo : pb::IMessage {
    private static readonly pb::MessageParser<LinkmanInfo> _parser = new pb::MessageParser<LinkmanInfo>(() => new LinkmanInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<LinkmanInfo> Parser { get { return _parser; } }

    /// <summary>Field number for the "RolesID" field.</summary>
    public const int RolesIDFieldNumber = 1;
    private string rolesID_ = "";
    /// <summary>
    ///角色ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string RolesID {
      get { return rolesID_; }
      set {
        rolesID_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "TargetID" field.</summary>
    public const int TargetIDFieldNumber = 2;
    private string targetID_ = "";
    /// <summary>
    ///目标ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string TargetID {
      get { return targetID_; }
      set {
        targetID_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "RelationalType" field.</summary>
    public const int RelationalTypeFieldNumber = 3;
    private int relationalType_;
    /// <summary>
    ///关系类型:0好友 1申请中
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int RelationalType {
      get { return relationalType_; }
      set {
        relationalType_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (RolesID.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(RolesID);
      }
      if (TargetID.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(TargetID);
      }
      if (RelationalType != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(RelationalType);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (RolesID.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(RolesID);
      }
      if (TargetID.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TargetID);
      }
      if (RelationalType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RelationalType);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            RolesID = input.ReadString();
            break;
          }
          case 18: {
            TargetID = input.ReadString();
            break;
          }
          case 24: {
            RelationalType = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed class HeroInfo : pb::IMessage {
    private static readonly pb::MessageParser<HeroInfo> _parser = new pb::MessageParser<HeroInfo>(() => new HeroInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HeroInfo> Parser { get { return _parser; } }

    /// <summary>Field number for the "RolesID" field.</summary>
    public const int RolesIDFieldNumber = 1;
    private int rolesID_;
    /// <summary>
    ///角色ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int RolesID {
      get { return rolesID_; }
      set {
        rolesID_ = value;
      }
    }

    /// <summary>Field number for the "ID" field.</summary>
    public const int IDFieldNumber = 2;
    private int iD_;
    /// <summary>
    ///英雄ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ID {
      get { return iD_; }
      set {
        iD_ = value;
      }
    }

    /// <summary>Field number for the "Name" field.</summary>
    public const int NameFieldNumber = 3;
    private string name_ = "";
    /// <summary>
    ///创建的名称
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (RolesID != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(RolesID);
      }
      if (ID != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(ID);
      }
      if (Name.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Name);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (RolesID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RolesID);
      }
      if (ID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ID);
      }
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            RolesID = input.ReadInt32();
            break;
          }
          case 16: {
            ID = input.ReadInt32();
            break;
          }
          case 26: {
            Name = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed class PropsInfo : pb::IMessage {
    private static readonly pb::MessageParser<PropsInfo> _parser = new pb::MessageParser<PropsInfo>(() => new PropsInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<PropsInfo> Parser { get { return _parser; } }

    /// <summary>Field number for the "RolesID" field.</summary>
    public const int RolesIDFieldNumber = 1;
    private int rolesID_;
    /// <summary>
    ///角色ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int RolesID {
      get { return rolesID_; }
      set {
        rolesID_ = value;
      }
    }

    /// <summary>Field number for the "PropsType" field.</summary>
    public const int PropsTypeFieldNumber = 2;
    private int propsType_;
    /// <summary>
    ///类型ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int PropsType {
      get { return propsType_; }
      set {
        propsType_ = value;
      }
    }

    /// <summary>Field number for the "ID" field.</summary>
    public const int IDFieldNumber = 3;
    private int iD_;
    /// <summary>
    ///道具ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ID {
      get { return iD_; }
      set {
        iD_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (RolesID != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(RolesID);
      }
      if (PropsType != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(PropsType);
      }
      if (ID != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(ID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (RolesID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(RolesID);
      }
      if (PropsType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(PropsType);
      }
      if (ID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ID);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            RolesID = input.ReadInt32();
            break;
          }
          case 16: {
            PropsType = input.ReadInt32();
            break;
          }
          case 24: {
            ID = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed class ChatInfo : pb::IMessage {
    private static readonly pb::MessageParser<ChatInfo> _parser = new pb::MessageParser<ChatInfo>(() => new ChatInfo());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ChatInfo> Parser { get { return _parser; } }

    /// <summary>Field number for the "ID" field.</summary>
    public const int IDFieldNumber = 1;
    private int iD_;
    /// <summary>
    ///消息ID
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ID {
      get { return iD_; }
      set {
        iD_ = value;
      }
    }

    /// <summary>Field number for the "Channel" field.</summary>
    public const int ChannelFieldNumber = 2;
    private int channel_;
    /// <summary>
    ///频道:0世界 1好友 2队伍 3房间所有人
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Channel {
      get { return channel_; }
      set {
        channel_ = value;
      }
    }

    /// <summary>Field number for the "MsgType" field.</summary>
    public const int MsgTypeFieldNumber = 3;
    private string msgType_ = "";
    /// <summary>
    ///消息类型:0文本 1图片 2语音 3表情
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string MsgType {
      get { return msgType_; }
      set {
        msgType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Msg" field.</summary>
    public const int MsgFieldNumber = 4;
    private string msg_ = "";
    /// <summary>
    ///聊天内容 如果是图片和语音都是url
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Msg {
      get { return msg_; }
      set {
        msg_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "SendID" field.</summary>
    public const int SendIDFieldNumber = 5;
    private int sendID_;
    /// <summary>
    ///发送玩家
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int SendID {
      get { return sendID_; }
      set {
        sendID_ = value;
      }
    }

    /// <summary>Field number for the "ReceiveID" field.</summary>
    public const int ReceiveIDFieldNumber = 6;
    private int receiveID_;
    /// <summary>
    ///接收的玩家，当发送给好友时进行赋值
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ReceiveID {
      get { return receiveID_; }
      set {
        receiveID_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (ID != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(ID);
      }
      if (Channel != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Channel);
      }
      if (MsgType.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(MsgType);
      }
      if (Msg.Length != 0) {
        output.WriteRawTag(34);
        output.WriteString(Msg);
      }
      if (SendID != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(SendID);
      }
      if (ReceiveID != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(ReceiveID);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (ID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ID);
      }
      if (Channel != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Channel);
      }
      if (MsgType.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(MsgType);
      }
      if (Msg.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Msg);
      }
      if (SendID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(SendID);
      }
      if (ReceiveID != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ReceiveID);
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 8: {
            ID = input.ReadInt32();
            break;
          }
          case 16: {
            Channel = input.ReadInt32();
            break;
          }
          case 26: {
            MsgType = input.ReadString();
            break;
          }
          case 34: {
            Msg = input.ReadString();
            break;
          }
          case 40: {
            SendID = input.ReadInt32();
            break;
          }
          case 48: {
            ReceiveID = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code