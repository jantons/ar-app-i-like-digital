//------------------------------------------------------------------------------
// <auto-generated />
//
// This file was automatically generated by SWIG (http://www.swig.org).
// Version 3.0.12
//
// Do not make changes to this file unless you know what you are doing--modify
// the SWIG interface file instead.
//------------------------------------------------------------------------------


public class vx_req_account_web_call_t : global::System.IDisposable {
  private global::System.Runtime.InteropServices.HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal vx_req_account_web_call_t(global::System.IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new global::System.Runtime.InteropServices.HandleRef(this, cPtr);
  }

  internal static global::System.Runtime.InteropServices.HandleRef getCPtr(vx_req_account_web_call_t obj) {
    return (obj == null) ? new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero) : obj.swigCPtr;
  }

  ~vx_req_account_web_call_t() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != global::System.IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          VivoxCoreInstancePINVOKE.delete_vx_req_account_web_call_t(swigCPtr);
        }
        swigCPtr = new global::System.Runtime.InteropServices.HandleRef(null, global::System.IntPtr.Zero);
      }
      global::System.GC.SuppressFinalize(this);
    }
  }

        public static implicit operator vx_req_base_t(vx_req_account_web_call_t t) {
            return t.base_;
        }
        
  public vx_req_base_t base_ {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_base__set(swigCPtr, vx_req_base_t.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_base__get(swigCPtr);
      vx_req_base_t ret = (cPtr == global::System.IntPtr.Zero) ? null : new vx_req_base_t(cPtr, false);
      return ret;
    } 
  }

  public string account_handle {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_account_handle_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_account_handle_get(swigCPtr);
      return ret;
    } 
  }

  public string relative_path {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_relative_path_set(swigCPtr, value);
    } 
    get {
      string ret = VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_relative_path_get(swigCPtr);
      return ret;
    } 
  }

  public int parameter_count {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_parameter_count_set(swigCPtr, value);
    } 
    get {
      int ret = VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_parameter_count_get(swigCPtr);
      return ret;
    } 
  }

  public SWIGTYPE_p_p_vx_name_value_pair parameters {
    set {
      VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_parameters_set(swigCPtr, SWIGTYPE_p_p_vx_name_value_pair.getCPtr(value));
    } 
    get {
      global::System.IntPtr cPtr = VivoxCoreInstancePINVOKE.vx_req_account_web_call_t_parameters_get(swigCPtr);
      SWIGTYPE_p_p_vx_name_value_pair ret = (cPtr == global::System.IntPtr.Zero) ? null : new SWIGTYPE_p_p_vx_name_value_pair(cPtr, false);
      return ret;
    } 
  }

  public vx_req_account_web_call_t() : this(VivoxCoreInstancePINVOKE.new_vx_req_account_web_call_t(), true) {
  }

}
