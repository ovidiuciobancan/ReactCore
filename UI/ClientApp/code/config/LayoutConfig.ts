import * as Mui from 'material-ui'
import * as RRD from 'react-router-dom'
import colors from 'css/js/material-ui/colors'

import {
    LayoutViewProps,
    AppBarProps,
    NavMenuProps,
} from 'interfaces/ComponentProps';

export const defaultLayoutConfiguration: LayoutViewProps = <LayoutViewProps>{
    appBarProps: <AppBarProps>{
        config: <Mui.AppBarProps>{
            style: {
                height: 65
            },
            iconStyleLeft: {
                //marginLeft: 200
            },
            iconElementRight: undefined, //need to be setup by higher component
            iconStyleRight: {
                marginTop: 0,
                marginRight: 50
            },
            type: 'persistent'
        }
    },
    navMenuProps: <NavMenuProps>{
        items: [],
        drawerConfig: <Mui.DrawerProps>{
            open: true,
            containerStyle: {
                padding: 0,
                overflow: 'hidden',
                backgroundColor: colors.darkBlue,
                height: 'calc(100% - 65px)',
                top: 65
            },
            width: 300,
            style: {
                marginTop: 50
            }
        },
        menuItemProps: <Mui.MenuItemProps>{
            innerDivStyle: {
                padding: 0,
            }
        },
        navLinkProps: <RRD.NavLinkProps | any>{
            activeStyle: {
                backgroundColor: 'white',
                display: 'block',
                color: colors.darkBlue
            },
            style: {
                color: 'white',
                paddind: 10
            }
        }
    }
} as LayoutViewProps