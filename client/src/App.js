import React from 'react';
import './App.css';
import { BrowserRouter , Route, Switch } from "react-router-dom";
// import routes from './Components/router';
// import Menu from './path/to/your/MenuComponent';  // Thay đường dẫn bằng đường dẫn thực tế
import LandingPage from './Screnes/landingPage';
import loginPage from './Screnes/Login/loginPage';

function App() {
  return (
    <div className="App">
      <BrowserRouter>
          {/* Menu */}
          {/* <Menu /> */}
          {/* Nội dung */}
          <Switch>
          <Route path="/" exact component={LandingPage} />
          <Route path="/login" component={loginPage} />
          </Switch>
      </BrowserRouter>
      <footer className="App-footer"></footer>
    </div>
  );
}

// const showContentMenu = (routes) => {
//   var result = null;

//   if (routes.length > 0) {
//     result = routes.map((route, index) => (
//       <Route
//         key={index}
//         path={route.path}
//         exact={route.exact}
//         component={route.main}
//       />
//     ));
//   }

//   return result;
// }

export default App;