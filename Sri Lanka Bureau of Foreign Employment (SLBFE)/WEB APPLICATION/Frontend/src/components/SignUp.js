import React, { Component } from "react";
import "./SignIn&SignUp.scss";
import AuthServices from "../services/AuthServices.js";

// Import materials
import TextField from "@material-ui/core/TextField";
import Radio from "@material-ui/core/Radio";
import RadioGroup from "@material-ui/core/RadioGroup";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Button from "@material-ui/core/Button";
import Snackbar from "@material-ui/core/Snackbar";
import IconButton from "@material-ui/core/IconButton";
import CloseIcon from "@material-ui/icons/Close";

const authServices = new AuthServices();

export default class SignUp extends Component {
  constructor() {
    super();
    this.state = {
      Radiovalue: "Citizen",
      NIC: "",
      Name: "",
      Address: "",
      Age: "",
      Profession: "",
      Email: "",
      Password: "",
      ConfirmPassword: "",
      Qualification: "",

      NICFlag: false,
      NameFlag: false,
      AddressFlag: false,
      AgeFlag: false,
      ProfessionFlag: false,
      EmailFlag: false,
      PasswordFlag: false,
      ConfirmPasswordFlag: false,
      QualificationFlag: false,

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

  // Fields Validating
  CheckValidity() {
    console.log("Check Validity Calling");
    //Reset Flag
    this.setState({
      NICFlag: false,
      NameFlag: false,
      AddressFlag: false,
      AgeFlag: false,
      ProfessionFlag: false,
      EmailFlag: false,
      PasswordFlag: false,
      ConfirmPasswordFlag: false,
      QualificationFlag: false,
    });

    if (this.state.NIC === "") {
      this.setState({ NICFlag: true });
    }
    if (this.state.Name === "") {
      this.setState({ NameFlag: true });
    }
    if (this.state.Address === "") {
      this.setState({ AddressFlag: true });
    }
    if (this.state.Age === "") {
      this.setState({ AgeFlag: true });
    }
    if (this.state.Profession === "") {
      this.setState({ ProfessionFlag: true });
    }
    if (this.state.Email === "") {
      this.setState({ EmailFlag: true });
    }
    if (this.state.Password === "") {
      this.setState({ PasswordFlag: true });
    }
    if (this.state.ConfirmPassword === "") {
      this.setState({ ConfirmPasswordFlag: true });
    }
    if (this.state.Qualification === "") {
      this.setState({ QualificationFlag: true });
    }
  }

  // Submit handle
  handleSubmit = (e) => {
    this.CheckValidity();
    if (
      this.state.NIC !== "" &&
      this.state.Name !== "" &&
      this.state.Address !== "" &&
      this.state.Age !== "" &&
      this.state.Profession !== "" &&
      this.state.Email !== "" &&
      this.state.Password !== "" &&
      this.state.ConfirmPassword !== "" &&
      this.state.Qualification !== ""
    ) {
      const data = {
        nic: this.state.NIC,
        name: this.state.Name,
        address: this.state.Address,
        age: this.state.Age,
        profession: this.state.Profession,
        email: this.state.Email,
        password: this.state.Password,
        configPassword: this.state.ConfirmPassword,
        affiliation: this.state.Radiovalue,
        qualification: this.state.Qualification,
      };

      authServices
        .SignUp(data)
        .then((data) => {
          console.log("data : ", data);
          if (data.data.isSuccess) {
            this.props.history.push("/SignIn");
          } else {
            console.log("Sign Up Failed");
            this.setState({ open: true, Message: "Sign Up Failed" });
          }
        })
        .catch((error) => {
          console.log("error : ", error);
          this.setState({ open: true, Message: "Something Went Wrong" });
        });
    } else {
      console.log("Not Acceptable");
      this.setState({ open: true, Message: "Please Fill Required Field" });
    }
  };

  // Handle Radio butons change
  handleRadioChange = (e) => {
    this.setState({ Radiovalue: e.target.value });
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

  // Admin SignIn path
  handleAdminSignIn = (e) => {
    this.props.history.push("/AdminSignIn");
  };

  render() {
    console.log("state : ", this.state);
    return (
      <div className="SignUp-Container">
        <div className="SignUp-SubContainer">
          <div className="Header">Sign Up</div>
          <div className="Body">
            <form className="form">
              <Button
                id="adminLogin"
                className="Btn"
                color="primary"
                onClick={this.handleAdminSignIn}
              >
                Admin
              </Button>
              <TextField
                className="TextField"
                name="NIC"
                label="NIC"
                variant="outlined"
                size="small"
                error={this.state.NICFlag}
                value={this.state.NIC}
                onChange={this.handleChange}
              />
              <TextField
                className="TextField"
                name="Name"
                label="Name"
                variant="outlined"
                size="small"
                error={this.state.NameFlag}
                value={this.state.Name}
                onChange={this.handleChange}
              />
              <TextField
                className="TextField"
                name="Address"
                label="Address"
                variant="outlined"
                size="small"
                error={this.state.AddressFlag}
                value={this.state.Address}
                onChange={this.handleChange}
              />
              <TextField
                className="TextField"
                name="Age"
                label="Age"
                variant="outlined"
                size="small"
                error={this.state.AgeFlag}
                value={this.state.Age}
                onChange={this.handleChange}
              />
              <TextField
                className="TextField"
                name="Profession"
                label="Profession"
                variant="outlined"
                size="small"
                error={this.state.ProfessionFlag}
                value={this.state.Profession}
                onChange={this.handleChange}
              />
              <TextField
                className="TextField"
                type="Email"
                name="Email"
                label="Email"
                variant="outlined"
                size="small"
                error={this.state.EmailFlag}
                value={this.state.Email}
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
              <TextField
                className="TextField"
                type="password"
                name="ConfirmPassword"
                label="Confirm Password"
                variant="outlined"
                size="small"
                error={this.state.ConfirmPasswordFlag}
                value={this.state.ConfirmPassword}
                onChange={this.handleChange}
              />
              <TextField
                className="TextField"
                name="Qualification"
                label="Qualification"
                variant="outlined"
                size="small"
                error={this.state.QualificationFlag}
                value={this.state.Qualification}
                onChange={this.handleChange}
              />
              <RadioGroup
                className="Affiliations"
                name="Affiliation"
                value={this.state.Radiovalue}
                onChange={this.handleRadioChange}
              >
                <FormControlLabel
                  className="AffiliationValue"
                  value="Officer"
                  control={<Radio />}
                  label="Officer"
                />
                <FormControlLabel
                  className="AffiliationValue"
                  value="Citizen"
                  control={<Radio />}
                  label="Citizen"
                />
              </RadioGroup>
            </form>
          </div>
          <div className="Buttons">
            <Button className="Btn" color="primary" onClick={this.handleSignIn}>
              Sign In
            </Button>
            <Button
              className="Btn"
              variant="contained"
              color="primary"
              onClick={this.handleSubmit}
            >
              Sign Up
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
