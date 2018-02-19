import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';
import { DrawerWidget } from 'widgets/drawer'
import { DrawerProps, MenuItem } from 'material-ui';
import { NavMenuProps } from 'interfaces/ComponentProps'

export const NavMenu = (props: NavMenuProps) => {

    const menuItems = props.items || [];

    return (
        <DrawerWidget config={props.drawerConfig}>
            {menuItems.map(item => (
                <MenuItem {...props.menuItemProps}>
                    <NavLink exact={item.exact} to={item.routeTo} activeClassName='active' {...props.navLinkProps}>
                        <div className='row'>
                            <div className='col-sm-1'>
                            </div>
                            <div className='col-sm-2'>
                                <span className={`glyphicon ${item.glyphiconTag || 'glyphicon-list'}`}></span>
                            </div>
                            <div className='col-sm-6'>
                                {item.content}
                            </div>
                        </div>
                    </NavLink>
                </MenuItem>
            ))}
        </DrawerWidget>
    );
}
