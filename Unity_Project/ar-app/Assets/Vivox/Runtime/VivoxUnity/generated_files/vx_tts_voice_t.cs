//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class vx_tts_voice_t : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal vx_tts_voice_t(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(vx_tts_voice_t obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~vx_tts_voice_t() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VivoxCoreInstancePINVOKE.delete_vx_tts_voice_t(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

  public uint voice_id {
    set {
      VivoxCoreInstancePINVOKE.vx_tts_voice_t_voice_id_set(swigCPtr, value);
    } 
    get {
      uint ret = VivoxCoreInstancePINVOKE.vx_tts_voice_t_voice_id_get(swigCPtr);
      return ret;
    } 
  }

  public string name {
    set {
      VivoxCoreInstancePINVOKE.vx_tts_voice_t_name_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_tts_voice_t_name_get(swigCPtr);
      return ret;
    } 
  }

  public vx_tts_voice_t() : this(VivoxCoreInstancePINVOKE.new_vx_tts_voice_t(), true) {
  }

}
