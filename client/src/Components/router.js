import loginPage from "../Screnes/Login/loginPage";
import React from 'react';

// import home
const routes = [
    // {
    //     path : '/',
    //     exact : true,
    //     main : () => <Home />
    // },
    // {
    //     path : '/about',
    //     exact : false,
    //     main : () => <About />
    // },
    // {
    //     path : '/contact',
    //     exact : false,
    //     main : () => <Contact />
    // },
    // {
    //     path : '/notfound',
    //     exact : false,
    //     main : () => <NotFound />
    // },
    // {
    //     path : '/products',
    //     exact : false,
    //     main : ({ match, location }) => <Products match={match} location={location} />
    // },
    {
        path : '/login',
        exact : false,
        main : ({location}) => <loginPage location={location} />
    }
];

export default routes;