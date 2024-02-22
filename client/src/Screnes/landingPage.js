import React from "react";
import { Link } from "react-router-dom/cjs/react-router-dom";

function landingPage() {
  return (
    <>
      <h1>What are you waiting for?</h1>
      <Link to="/login">Login
      </Link>    </>
  );
}
export default landingPage;