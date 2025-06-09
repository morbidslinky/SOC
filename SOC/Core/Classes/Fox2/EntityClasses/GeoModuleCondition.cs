using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Classes.Fox2
{
    class GeoModuleCondition : Fox2EntityClass
    {
        private string name;
        private bool isPlayerTriggerOnly;
        private Fox2EntityClass dataSet, transform, checkCallback, parent;

        public GeoModuleCondition(string _name, bool _isPlayerTriggerOnly, Fox2EntityClass _dataSet, Fox2EntityClass _parent)
        {
            name = _name; dataSet = _dataSet; parent = _parent; isPlayerTriggerOnly = _isPlayerTriggerOnly;
        }

        public void SetTransform(Fox2EntityClass t)
        {
            transform = t;
        }

        public void SetcheckCallback(Fox2EntityClass c)
        {
            checkCallback = c;
        }

        public override string GetFox2Format()
        {
            return string.Format($@"
        <entity class=""GeoModuleCondition"" classVersion=""0"" addr=""{GetHexAddress()}"" unknown1=""352"" unknown2=""249890"">
          <staticProperties>
            <property name=""name"" type=""String"" container=""StaticArray"" arraySize=""1"">
              <value>{name}</value>
            </property>
            <property name=""dataSet"" type=""EntityHandle"" container=""StaticArray"" arraySize=""1"">
              <value>{dataSet.GetHexAddress()}</value>
            </property>
            <property name=""parent"" type=""EntityHandle"" container=""StaticArray"" arraySize=""1"">
              <value>{parent.GetHexAddress()}</value>
            </property>
            <property name=""transform"" type=""EntityPtr"" container=""StaticArray"" arraySize=""1"">
              <value>{transform.GetHexAddress()}</value>
            </property>
            <property name=""shearTransform"" type=""EntityPtr"" container=""StaticArray"" arraySize=""1"">
              <value>0x00000000</value>
            </property>
            <property name=""pivotTransform"" type=""EntityPtr"" container=""StaticArray"" arraySize=""1"">
              <value>0x00000000</value>
            </property>
            <property name=""children"" type=""EntityHandle"" container=""List"" />
            <property name=""flags"" type=""uint32"" container=""StaticArray"" arraySize=""1"">
              <value>7</value>
            </property>
            <property name=""trapCategory"" type=""String"" container=""StaticArray"" arraySize=""1"">
              <value></value>
            </property>
            <property name=""trapPriority"" type=""uint32"" container=""StaticArray"" arraySize=""1"">
              <value>0</value>
            </property>
            <property name=""enable"" type=""bool"" container=""StaticArray"" arraySize=""1"">
              <value>true</value>
            </property>
            <property name=""isOnce"" type=""bool"" container=""StaticArray"" arraySize=""1"">
              <value>false</value>
            </property>
            <property name=""isAndCheck"" type=""bool"" container=""StaticArray"" arraySize=""1"">
              <value>true</value>
            </property>
            <property name=""checkFuncNames"" type=""String"" container=""DynamicArray"" arraySize=""1"">
              <value>{(isPlayerTriggerOnly ? "IsPlayer" : "")}</value>
            </property>
            <property name=""execFuncNames"" type=""String"" container=""DynamicArray"" />
            <property name=""checkCallbackDataElements"" type=""EntityPtr"" container=""DynamicArray"" arraySize=""1"">
              <value>{checkCallback.GetHexAddress()}</value>
            </property>
            <property name=""execCallbackDataElements"" type=""EntityPtr"" container=""DynamicArray"" />
          </staticProperties>
          <dynamicProperties />
        </entity>
            ");
        }

        public override string GetName()
        {
            return name;
        }

        public override Fox2EntityClass GetOwner()
        {
            return dataSet;
        }
    }
}
