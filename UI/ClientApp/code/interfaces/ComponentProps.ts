import { NavLinkProps } from 'react-router-dom'
import * as Mui from 'material-ui'

export interface LayoutViewProps {
    children?: any
    appBarProps: AppBarProps,
    navMenuProps: NavMenuProps,
}

export interface AppBarProps {
    config: Mui.AppBarProps
}

export interface MenuItem {
    content: any
    routeTo: string
    exact?: boolean | false
    glyphiconTag?: string
}

export interface NavMenuProps {
    items: MenuItem[]
    drawerConfig?: Mui.DrawerProps,
    toolbarConfig?: Mui.ToolbarProps,
    menuItemProps?: Mui.MenuItemProps,
    navLinkProps?: NavLinkProps | any
}