// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: HeroPB.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using scg = global::System.Collections.Generic;
namespace ProtoMsg {

  #region Messages
  public sealed class HeroGetListC2S : pb::IMessage {
    private static readonly pb::MessageParser<HeroGetListC2S> _parser = new pb::MessageParser<HeroGetListC2S>(() => new HeroGetListC2S());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HeroGetListC2S> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
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
        }
      }
    }

  }

  public sealed class HeroGetListS2C : pb::IMessage {
    private static readonly pb::MessageParser<HeroGetListS2C> _parser = new pb::MessageParser<HeroGetListS2C>(() => new HeroGetListS2C());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HeroGetListS2C> Parser { get { return _parser; } }

    /// <summary>Field number for the "Result" field.</summary>
    public const int ResultFieldNumber = 1;
    private int result_;
    /// <summary>
    ///结果:0获取成功 1未存在英雄
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "HeroInfo" field.</summary>
    public const int HeroInfoFieldNumber = 2;
    private static readonly pb::FieldCodec<global::ProtoMsg.HeroInfo> _repeated_heroInfo_codec
        = pb::FieldCodec.ForMessage(18, global::ProtoMsg.HeroInfo.Parser);
    private readonly pbc::RepeatedField<global::ProtoMsg.HeroInfo> heroInfo_ = new pbc::RepeatedField<global::ProtoMsg.HeroInfo>();
    /// <summary>
    ///英雄列表
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::ProtoMsg.HeroInfo> HeroInfo {
      get { return heroInfo_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Result != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Result);
      }
      heroInfo_.WriteTo(output, _repeated_heroInfo_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Result);
      }
      size += heroInfo_.CalculateSize(_repeated_heroInfo_codec);
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
            Result = input.ReadInt32();
            break;
          }
          case 18: {
            heroInfo_.AddEntriesFrom(input, _repeated_heroInfo_codec);
            break;
          }
        }
      }
    }

  }

  public sealed class HeroBuyC2S : pb::IMessage {
    private static readonly pb::MessageParser<HeroBuyC2S> _parser = new pb::MessageParser<HeroBuyC2S>(() => new HeroBuyC2S());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HeroBuyC2S> Parser { get { return _parser; } }

    /// <summary>Field number for the "HeroInfo" field.</summary>
    public const int HeroInfoFieldNumber = 1;
    private global::ProtoMsg.HeroInfo heroInfo_;
    /// <summary>
    ///购买的英雄
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoMsg.HeroInfo HeroInfo {
      get { return heroInfo_; }
      set {
        heroInfo_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (heroInfo_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(HeroInfo);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (heroInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(HeroInfo);
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
            if (heroInfo_ == null) {
              heroInfo_ = new global::ProtoMsg.HeroInfo();
            }
            input.ReadMessage(heroInfo_);
            break;
          }
        }
      }
    }

  }

  public sealed class HeroBuyS2C : pb::IMessage {
    private static readonly pb::MessageParser<HeroBuyS2C> _parser = new pb::MessageParser<HeroBuyS2C>(() => new HeroBuyS2C());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HeroBuyS2C> Parser { get { return _parser; } }

    /// <summary>Field number for the "HeroInfo" field.</summary>
    public const int HeroInfoFieldNumber = 1;
    private global::ProtoMsg.HeroInfo heroInfo_;
    /// <summary>
    ///购买的英雄
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::ProtoMsg.HeroInfo HeroInfo {
      get { return heroInfo_; }
      set {
        heroInfo_ = value;
      }
    }

    /// <summary>Field number for the "Result" field.</summary>
    public const int ResultFieldNumber = 2;
    private int result_;
    /// <summary>
    ///结果:0购买成功 1失败:角色名称已存在 2失败:名称存在敏感词 3失败:货币不足
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (heroInfo_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(HeroInfo);
      }
      if (Result != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Result);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (heroInfo_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(HeroInfo);
      }
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Result);
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
            if (heroInfo_ == null) {
              heroInfo_ = new global::ProtoMsg.HeroInfo();
            }
            input.ReadMessage(heroInfo_);
            break;
          }
          case 16: {
            Result = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
