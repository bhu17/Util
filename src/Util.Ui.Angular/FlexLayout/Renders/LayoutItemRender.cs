﻿using System.Linq;
using Util.Ui.Angular;
using Util.Ui.Builders;
using Util.Ui.Configs;
using Util.Ui.Extensions;
using Util.Ui.FlexLayout.Enums;
using Util.Ui.Renders;

namespace Util.Ui.FlexLayout.Renders {
    /// <summary>
    /// 浮动布局项渲染器
    /// </summary>
    public class LayoutItemRender : RenderBase {
        /// <summary>
        /// 配置
        /// </summary>
        private readonly IConfig _config;

        /// <summary>
        /// 初始化浮动布局项渲染器
        /// </summary>
        /// <param name="config">配置</param>
        public LayoutItemRender( IConfig config ) : base( config ) {
            _config = config;
        }

        /// <summary>
        /// 获取标签生成器
        /// </summary>
        protected override TagBuilder GetTagBuilder() {
            var builder = new DivBuilder();
            Config( builder );
            return builder;
        }

        /// <summary>
        /// 配置
        /// </summary>
        protected void Config( TagBuilder builder ) {
            builder.Style( _config );
            builder.Class( _config );
            builder.AddOutputAttributes( _config );
            ConfigId( builder );
            ConfigFlex( builder );
            ConfigOrder( builder );
            ConfigOffset( builder );
            ConfigAlign( builder );
            ConfigFill( builder );
            ConfigAngular( builder );
            ConfigContent( builder );
        }

        /// <summary>
        /// 配置尺寸调整策略
        /// </summary>
        private void ConfigFlex( TagBuilder builder ) {
            if ( _config.AllAttributes.Any( t => t.Name.StartsWith( UiConst.Flex ) ) == false ) {
                builder.AddAttribute( "fxFlex", "1 1 auto" );
                return;
            }
            builder.AddAttribute( "fxFlex", _config.GetValue( UiConst.Flex ) );
            Util.Helpers.Enum.GetItems<BreakpointAlias>().ForEach( t => builder.AddAttribute( $"fxFlex.{t.Text}", _config.GetValue( $"{UiConst.Flex}-{t.Text}" ) ) );
        }

        /// <summary>
        /// 配置排序
        /// </summary>
        private void ConfigOrder( TagBuilder builder ) {
            builder.AddAttribute( "fxFlexOrder", _config.GetValue( UiConst.Order ) );
        }

        /// <summary>
        /// 配置偏移量
        /// </summary>
        private void ConfigOffset( TagBuilder builder ) {
            builder.AddAttribute( "fxFlexOffset", _config.GetValue( UiConst.Offset ) );
        }

        /// <summary>
        /// 配置对齐方式
        /// </summary>
        private void ConfigAlign( TagBuilder builder ) {
            builder.AddAttribute( "fxFlexAlign", _config.GetValue<FlexAlign?>( UiConst.Align )?.Description() );
        }

        /// <summary>
        /// 配置填充
        /// </summary>
        private void ConfigFill( TagBuilder builder ) {
            if( _config.GetValue<bool?>( UiConst.Fill ) == true )
                builder.AddAttribute( "fxFlexFill" );
        }

        /// <summary>
        /// 配置angular属性
        /// </summary>
        private void ConfigAngular( TagBuilder builder ) {
            builder.NgIf( _config );
            builder.NgFor( _config );
        }
    }
}