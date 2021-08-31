﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// TreeItem 组件
    /// </summary>
    public class TreeItem
    {
        /// <summary>
        /// 获得 父级节点
        /// </summary>
        public TreeItem? Parent { get; set; }

        /// <summary>
        /// 获得/设置 是否显示正在加载动画 默认为 false
        /// </summary>
        public bool ShowLoading { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected List<TreeItem> _items = new List<TreeItem>();
        /// <summary>
        /// 获得/设置 子节点数据源
        /// </summary>
        public virtual List<TreeItem> Items { get => _items; set => _items = value; } 

        /// <summary>
        /// 获得/设置 TreeItem 标识
        /// </summary>
        public object? Key { get; set; }

        /// <summary>
        /// 获得/设置 TreeItem 相关额外信息
        /// </summary>
        public object? Tag { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        public virtual string? Text { get; set; }

        /// <summary>
        /// 获得/设置 图标
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 是否激活
        /// </summary>
        /// <value></value>
        public bool IsActive { get; set; }

        /// <summary>
        /// 获得/设置 是否被选中
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 获得/设置 是否被禁用 默认 false
        /// </summary>
        /// <value></value>
        [Obsolete("请使用 IsDisabled 属性，本属性已过期", true)]
        public bool Disabled { get; set; }

        /// <summary>
        /// 获得/设置 是否被禁用 默认 false
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 是否展开 默认 false 不展开
        /// </summary>
        public bool IsExpanded {
            get;
            set; }

        /// <summary>
        /// 获取/设置 是否有子节点 默认 false 
        /// </summary>
        public bool HasChildNode { get; set; }

        /// <summary>
        /// 添加 TreeItem 方法 由 TreeItem 方法加载时调用
        /// </summary>
        /// <param name="item">Menutem 实例</param>
        public void AddItem(TreeItem item)
        {
            item.Parent = this;
            _items.Add(item);
        }
        /// <summary>
        /// 移除 TreeItem
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(TreeItem item)
        {
            if (item.Parent!=null)
            {
                item.Parent.Items.Remove(item);
            }
        }

        /// <summary>
        /// 获得 所有子项集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TreeItem> GetAllSubItems() => Items.Concat(GetSubItems(Items));

        private static IEnumerable<TreeItem> GetSubItems(List<TreeItem> items) => items.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetSubItems(i.Items)) : i.Items);

        /// <summary>
        /// 级联设置复选状态
        /// </summary>
        public void CascadeSetCheck(bool isChecked)
        {
            foreach (var item in Items)
            {
                item.Checked = isChecked;
                if (item.Items.Any())
                {
                    item.CascadeSetCheck(isChecked);
                }
            }
        }

        /// <summary>
        /// 级联设置展开状态方法
        /// </summary>
        public void CollapseOtherNodes()
        {
            if (Parent != null)
            {
                foreach (var node in Parent.Items.Where(p => p.IsExpanded && p != this))
                {
                    node.IsExpanded = false;
                }
            }
        }
    }
}
