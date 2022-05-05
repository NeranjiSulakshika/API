import React, { Component } from 'react'
import AuthServices from '../services/AuthServices'
import './SignUp.scss'
import TextField from '@material-ui/core/TextField'
import Button from '@material-ui/core/Button'
import Snackbar from '@material-ui/core/Snackbar'
import IconButton from '@material-ui/core/IconButton'
import CloseIcon from '@material-ui/icons/Close'

const authServices = new AuthServices()

export default class AdminSignIn extends Component {
  constructor() {
    super()
    this.state = {
      UserName: '',
      UserNameFlag: false,
      Password: '',
      PasswordFlag: false,
      open: false,
      Message: '',
    }
  }

  handleClose = (e, reason) => {
    if (reason === 'clickaway') {
      return
    }
    this.setState({ open: false })
  }

  handleChange = (e) => {
    const { name, value } = e.target
    this.setState(
      { [name]: value },
      console.log('Name : ', name, 'Value : ', value),
    )
  }

  handleSignIn = (e) => {
    this.props.history.push('/SignIn')
  }

  CheckValidation() {
    console.log('CheckValidation Calling...')

    this.setState({ UserNameFlag: false, PasswordFlag: false })

    if (this.state.UserName === '') {
      this.setState({ UserNameFlag: true })
    }
    if (this.state.Password === '') {
      this.setState({ PasswordFlag: true })
    }
  }

  handleSubmit = (e) => {
    this.CheckValidation()
    if (this.state.UserName !== '' && this.state.Password !== '') {
      console.log('Acceptable')
      let data = {
        username: this.state.UserName,
        password: this.state.Password,
      }
      authServices
        .AdminSignIn(data)
        .then((data) => {
          console.log('Data : ', data)
          if (data.data.isSuccess) {
            this.props.history.push('/AdminHomePage')
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
      this.setState({ open: true, Message: 'Please Field Mandetory Field' })
    }
  }

  render() {
    console.log('State : ', this.state)
    return (
      <div className="SignUp-Container">
        <div className="SignUp-SubContainer">
          <div className="Header">Admin Sign In</div>
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
          <div className="Buttons" style={{ alignItems: 'flex-start' }}>
            <Button className="Btn" color="primary" onClick={this.handleSignIn}>
              User Sign In
            </Button>
            <Button
              className="Btn"
              variant="contained"
              color="primary"
              onClick={this.handleSubmit}
            >
              Admin Sign In
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
