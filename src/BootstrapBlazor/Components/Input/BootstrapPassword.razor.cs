﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// PasswordBoxTextBase 组件
    /// </summary>
    public partial class BootstrapPassword
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("form-control")
            .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None && !IsDisabled && !IsValid.HasValue)
            .AddClass(CssClass).AddClass(ValidCss)
            .Build();

        /// <summary>
        /// 获得/设置 input 类型 placeholder 属性
        /// </summary>
        protected string? PlaceHolder { get; set; }

        /// <summary>
        /// 获得/设置 是否为 Input-Group 组合
        /// </summary>
        [Parameter]
        public bool IsGroup { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.None;

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("placeholder", out var ph))
            {
                PlaceHolder = ph?.ToString();
            }

            if (string.IsNullOrEmpty(PlaceHolder) && FieldIdentifier.HasValue)
            {
                PlaceHolder = FieldIdentifier.Value.GetPlaceHolder();
            }
        }
    }
}
