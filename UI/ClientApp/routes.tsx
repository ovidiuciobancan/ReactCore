import * as React from 'react'
import { Route } from 'react-router-dom'
import { Layout } from './components/Layout'
import Home from './components/Home'
import FetchData from './components/FetchData'
import Counter from './components/Counter'
import Authors from 'containers/authors/AuthorsPage'
import Author from 'containers/authors/AuthorPage'

import { OidcCallback } from 'providers/oidc'

export const paths = {
    home: '/',
    counter: '/counter',
    fetchData: '/fetchdata/:startDateIndex?',

    authors: '/authors',
    author: '/author/:id',

    oidcCallback: '/oidc-callback',

}


export const routes = <Layout>
    <Route exact path={paths.home} component={Home} />

    <Route path={paths.counter} component={Counter} />
    <Route path={paths.fetchData} component={FetchData} />
    <Route path={paths.oidcCallback} component={OidcCallback} />

    <Route path={paths.authors} component={Authors} />
    <Route path={paths.author} component={Author} />

</Layout>;
