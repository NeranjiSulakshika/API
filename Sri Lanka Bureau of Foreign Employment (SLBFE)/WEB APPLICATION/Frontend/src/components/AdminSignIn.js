import React, { Component } from "react";

// Import Services
import AuthServices from "../services/AuthServices";

// Import scss
import "./SignIn&SignUp.scss";

// Import materials
import TextField from "@material-ui/core/TextField";
import Button from "@material-ui/core/Button";
import Snackbar from "@material-ui/core/Snackbar";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";
import AppBar from "@material-ui/core/AppBar";
import Toolbar from "@material-ui/core/Toolbar";
import Typography from "@material-ui/core/Typography";

const authServices = new AuthServices();

export default class AdminSignIn extends Component {
  constructor() {
    super();
    this.state = {
      UserName: "",
      UserNameFlag: false,
      Password: "",
      PasswordFlag: false,
      open: false,
      Message: "",
    };
  }

  // Handle Close
  handleClose = (e, reason) => {
    if (reason === "clickaway") {
      return;
    }
    this.setState({ open: false });
  };

  handleChange = (e) => {
    const { name, value } = e.target;
    this.setState(
      { [name]: value },
      console.log("Name : ", name, "Value : ", value)
    );
  };

  // SignIn Handle
  handleSignIn = (e) => {
    this.props.history.push("/SignIn");
  };

  // Fields Validating
  CheckValidation() {
    console.log("CheckValidation Calling...");

    this.setState({ UserNameFlag: false, PasswordFlag: false });

    if (this.state.UserName === "") {
      this.setState({ UserNameFlag: true });
    }
    if (this.state.Password === "") {
      this.setState({ PasswordFlag: true });
    }
  }

  // Submit handle
  handleSubmit = (e) => {
    this.CheckValidation();
    if (this.state.UserName !== "" && this.state.Password !== "") {
      console.log("Acceptable");
      let data = {
        username: this.state.UserName,
        password: this.state.Password,
      };
      authServices
        .AdminSignIn(data)
        .then((data) => {
          console.log("Data : ", data);
          if (data.data.isSuccess) {
            this.props.history.push("/AdminHomePage");
          } else {
            console.log("Something Went Wrong");
            this.setState({ open: true, Message: "LogIn Failed" });
          }
        })
        .catch((error) => {
          console.log("Error : ", error);
          this.setState({ open: true, Message: "Something Went Wrong" });
        });
    } else {
      console.log("Not Acceptable");
      this.setState({ open: true, Message: "Please Fill Mandetory Field" });
    }
  };

  render() {
    console.log("State : ", this.state);
    return (
      <div className="SignUp-Container" id="Admin-SignUp-Container">
      <div>
        <AppBar position="static" className="navHeader">
          <Toolbar variant="dense">
            <Typography variant="h6" color="inherit" className="headerLabel">
              Sign-In
            </Typography>
          </Toolbar>
        </AppBar>
      </div>
        <div className="SignUp-SubContainer" id="Admin-SignUp-SubContainer">
          <div className="Header" id="Admin-Header">Admin Sign In</div>
          <div className="Body">
            <form className="form">
              <TextField
                className="TextField"
                name="UserName"
                label="UserName"
                variant="outlined"
                size="small"
                error={this.state.UserNameFlag}
                value={this.state.UserName}
                onChange={this.handleChange}
              />
              <TextField
                className="TextField"
                type="password"
                name="Password"
                label="Password"
                variant="outlined"
                size="small"
                error={this.state.PasswordFlag}
                value={this.state.Password}
                onChange={this.handleChange}
              />
            </form>
          </div>
          <div className="Buttons" style={{ alignItems: "flex-start" }}>
            <Button className="Btn" id="SignIn-Button" onClick={this.handleSignIn}>
              User Sign In
            </Button>
            <Button
              className="Btn"
              variant="contained"
              color="primary"
              id="Admin-Submit-Button"
              onClick={this.handleSubmit}
            >
              Admin Sign In
            </Button>
          </div>
        </div>
        <Snackbar
          anchorOrigin={{
            vertical: "bottom",
            horizontal: "left",
          }}
          open={this.state.open}
          autoHideDuration={6000}
          onClose={this.handleClose}
          message={this.state.Message}
          action={
            <React.Fragment>
              <Button color="secondary" size="small" onClick={this.handleClose}>
                UNDO
              </Button>
              <IconButton
                size="small"
                aria-label="close"
                color="inherit"
                onClick={this.handleClose}
              >
                <CloseIcon fontSize="small" />
              </IconButton>
            </React.Fragment>
          }
        />
      </div>
    );
  }
}
