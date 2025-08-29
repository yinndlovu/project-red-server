package projectred.controller;

import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import projectred.dto.UserRegistrationDto;
import projectred.model.User;
import projectred.service.UserService;

@RestController
@RequestMapping("/api/users")
public class UserController {

    @Autowired
    private UserService userService;

    @PostMapping("/register")
    public ResponseEntity<User> register(@Valid @RequestBody UserRegistrationDto dto) {
        User registeredUser = userService.registerUser(dto);
        return new ResponseEntity<>(registeredUser, HttpStatus.CREATED);
    }
}
