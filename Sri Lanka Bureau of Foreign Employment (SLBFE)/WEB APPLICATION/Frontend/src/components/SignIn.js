import React, { Component } from 'react'
import AuthServices from '../services/AuthServices'
import './SignIn&SignUp.scss'

// Import materials
import TextField from '@material-ui/core/TextField'
import Radio from '@material-ui/core/Radio'
import RadioGroup from '@material-ui/core/RadioGroup'
import FormControlLabel from '@material-ui/core/FormControlLabel'
import Button from '@material-ui/core/Button'
import Snackbar from '@material-ui/core/Snackbar'
import IconButton from '@material-ui/core/IconButton'
import CloseIcon from '@material-ui/icons/Close'

const authServices = new AuthServices()

export default class SignIn extends Component {
  constructor() {
    super()
    this.state = {
      Radiovalue: 'Citizen',
      Email: '',
      EmailFlag: false,
      Password: '',
      PasswordFlag: false,
      open: false,
      Message: '',
    }
  }

  // Handle Close
  handleClose = (e, reason) => {
    if (reason === 'clickaway') {
      return
    }
    this.setState({ open: false })
  }

  // Handle Radio butons change
  handleRadioChange = (e) => {
    this.setState({ Radiovalue: e.target.value })
  }

  handleChange = (e) => {
    const { name, value } = e.target
    this.setState(
      { [name]: value },
      console.log('Name : ', name, 'Value : ', value),
    )
  }

  // SignUp Handle
  handleSignUp = (e) => {
    this.props.history.push('/')
  }

  // Admin SignIn path
  handleAdminSignIn = (e) => {
    this.props.history.push('/AdminSignIn')
  }

  // Fields Validating
  CheckValidation() {
    console.log('CheckValidation Calling...')

    this.setState({ EmailFlag: false, PasswordFlag: false })

    if (this.state.Email === '') {
      this.setState({ EmailFlag: true })
    }
    if (this.state.Password === '') {
      this.setState({ PasswordFlag: true })
    }
  }

  // Submit button handle
  handleSubmit = (e) => {
    this.CheckValidation()
    if (this.state.Email !== '' && this.state.Password !== '') {
      console.log('Acceptable')
      let data = {
        email: this.state.Email,
        password: this.state.Password,
        affiliation: this.state.Radiovalue,
      }
      authServices
        .SignIn(data)
        .then((data) => {
          console.log('Data : ', data)
          if (data.data.isSuccess) {
            this.props.history.push('/HomePage')
          } else {
            console.log('Something Went Wrong')
            this.setState({ open: true, Message: 'LogIn Failed' })
          }
        })
        .catch((error) => {
          console.log('Error : ', error)
          this.setState({ open: true, Message: 'Something Went Wrong' })
        })
    } else {
      console.log('Not Acceptable')
      this.setState({ open: true, Message: 'Please Fill Mandetory Field' })
    }
  }

  render() {
    console.log('State : ', this.state)
    return (
      <div className="SignIn-Container">
        <div className="SignIn-SubContainer">
          <div className="SignIn-Header">Sign In</div>
          <div className="Body">
            <form className="form">
            <Button id="adminLogin" className="Btn" color="primary" onClick={this.handleAdminSignIn}>
              Admin
            </Button>

              <TextField
                className="TextField"
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
          <div className="Buttons" style={{ alignItems: 'flex-start' }}>
            <Button className="Btn" id='SignUp-Button' onClick={this.handleSignUp}>
              Sign Up
            </Button>
            <Button
              className="Btn"
              variant="contained"
              color="primary"
              id='Submit-Button'
              onClick={this.handleSubmit}
            >
              Sign In
            </Button>
          </div>
        </div>
        <Snackbar
          anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'left',
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
    )
  }
}
