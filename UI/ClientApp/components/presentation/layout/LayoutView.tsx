import * as React from 'react';
import { NavMenu } from 'presentation/layout/NavMenu';
import { AppBar } from 'presentation/layout/AppBar'
import {
    LayoutViewProps
} from 'interfaces/ComponentProps'

export const LayoutView = (props: LayoutViewProps) => {
    return (
        <div className='container-fluid'>
            <div className='row'>
                <AppBar {...props.appBarProps} />
            </div>
            <div className='row'>
                <div className='col-sm-2'>
                    <NavMenu {...props.navMenuProps} />
                </div>
                <div className='col-sm-10'>
                    {props.children}
                </div>
            </div>
        </div>
    );
}
