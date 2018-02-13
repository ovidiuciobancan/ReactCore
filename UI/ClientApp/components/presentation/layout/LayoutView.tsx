import * as React from 'react';
import { TopBar } from 'presentation/Layout/TopBar';
import { NavMenu } from 'presentation/Layout/NavMenu';

export const LayoutView = (props: any) => {
    return (
        <div className='container-fluid'>
            <div className='row'>
                <TopBar {...props} />
            </div>
            <div className='row'>
                <div className='col-sm-3'>
                    <NavMenu {...props} />
                </div>
                <div className='col-sm-9'>
                    {props.children}
                </div>
            </div>
        </div>
    );
}
