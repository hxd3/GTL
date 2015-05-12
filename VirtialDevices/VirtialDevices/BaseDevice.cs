﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VirtialDevices
{

    public enum DeviceStates { Running, Stop, Fault, Created };
    public enum DeviceType { Dispen, Clone, Matrix, Liquid, Analysis, Storage };

    public class EnumHelper 
    {
        public static DeviceType[] TypeEnums = { DeviceType.Dispen, DeviceType.Clone, DeviceType.Matrix, DeviceType.Liquid, DeviceType.Analysis, DeviceType.Storage };

        public static String getDeviceTypeString(DeviceType type) 
        {
            switch (type) 
            {
                case DeviceType.Analysis:
                    return "多通道高速代谢性能分析仪";
                case DeviceType.Clone:
                    return "单克隆挑选仪";
                case DeviceType.Dispen:
                    return "全自动培养基分装仪";
                case DeviceType.Liquid:
                    return "全自动液体处理工作站";
                case DeviceType.Matrix:
                    return "阵列式高通量培养仪";
                case DeviceType.Storage:
                    return "微孔板储存器";
            }
            return null;
        }

        public static String getDeviceStatusString(DeviceStates status) 
        {
            switch (status) 
            {
                case DeviceStates.Created:
                    return "已创建";
                case DeviceStates.Fault:
                    return "故障";
                case DeviceStates.Running:
                    return "正常";
                case DeviceStates.Stop:
                    return "停止";
            }
            return null;
        }
    }

    
    public class BaseDevice
    {

        protected VirtualDeviceManager virtualDeviceManager;

        public VirtualDeviceManager VirtualDeviceManager
        {
            get
            {
                return this.virtualDeviceManager;
            }
            set
            {
                this.virtualDeviceManager = value;
            }
        }
        private DeviceStates currentState;
        public DeviceStates CurrentState 
        {
            get 
            {
                return this.currentState;
            }
            set 
            {
                this.currentState = value;
            }
        }
        private DeviceType currentDeviceType;
        public DeviceType CurrentDeviceType 
        {
            get 
            {
                return this.currentDeviceType;
            }
            set 
            {
                this.currentDeviceType = value;
            }
        }

        private int ttl;

        public BaseDevice()
        {
            ttl = 0;
        }

        const int maxttl = 20;

        public void device_refresh()
        {
            ttl = 0;
        }

        public bool device_heartbeat()
        {
            ttl++;
            if (ttl < maxttl)
                return true;
            else
                return false;
        }

        private String ip;
        public String IP 
        {
            get 
            {
                return this.ip;
            }
            set 
            {
                this.ip = value;
            }
        }

        private String name;
        public String Name 
        {
            get 
            {
                return this.name;
            }
            set 
            {
                this.name = value;
            }
        }

        private String code;
        public String Code 
        {
            get 
            {
                return this.code;
            }
            set 
            {
                this.code = value;
            }
        }

        private String identifyID;
        public String IdentifyID 
        {
            get 
            {
                return this.identifyID;
            }
            set 
            {
                this.identifyID = value;
            }
        }

        private String serialID;
        public String SerialID 
        {
            get 
            {
                return this.serialID;
            }
            set 
            {
                this.serialID = value;
            }
        }

        private String controlIP;
        public String ControlIP 
        {
            get 
            {
                return this.controlIP;
            }
            set 
            {
                this.controlIP = value;
            }
        }

        private int sampleTime;
        public int SampleTime 
        {
            get 
            {
                return this.sampleTime;
            }
            set 
            {
                this.sampleTime = value;
            }
        }

        public virtual void SendMsg(String s) { }

        public virtual void ReceiveMsg(String s) { }
        public virtual void init() { }
        public virtual void deinit() { }

    }
}
