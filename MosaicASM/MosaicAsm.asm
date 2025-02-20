.data
    align 16
    mask_b dd 000000FFh, 000000FFh, 000000FFh, 000000FFh  ; Maska dla B
    mask_g dd 0000FF00h, 0000FF00h, 0000FF00h, 0000FF00h  ; Maska dla G
    mask_r dd 00FF0000h, 00FF0000h, 00FF0000h, 00FF0000h  ; Maska dla R

.code 
ApplyMosaic PROC
    ; Zapisz rejestry na stos
    push rbp
    mov rbp, rsp
    push rbx
    push rsi
    push rdi
    push r12
    push r13
    push r14
    push r15

    ; Zachowaj oryginalne parametry
    mov r12, rcx              ; sourceBuffer
    mov r13, rdx              ; resultBuffer
    mov r14, r8               ; height
    mov r15, r9               ; width

    ; Wczytaj argumenty ze stosu
    mov r10, [rbp + 48]   ; stride    
    mov r11, [rbp + 56]   ; start 
    mov r8, [rbp + 64]  ; stop
    mov r9, [rbp + 72]  ; tileSize

row_loop:
    cmp r11, r14        ;y >= heigth
    jge Exit
    cmp r11, r8         ;y >= stop
    jge Exit
    xor rcx, rcx        ;x = 0
col_loop:
    cmp rcx, r15        ;x >= width
    jge inc_row       
;///////////////////

    pxor xmm0, xmm0           ; R sum
    pxor xmm1, xmm1           ; G sum
    pxor xmm2, xmm2           ; B sum
    xor rbx, rbx         ; pixelCount = 0
    
         
;//////////////////////// PIERWSZA PÊTLA////////////////
    xor rdi, rdi         ; ty = 0
tile_row_loop:
    cmp rdi, r9          ; ty >= tileSize
    jge exit_tile_loop
    mov rax, r11
    add rax, rdi
    cmp rax, r14          ; y + ty >= heigth
    jge exit_tile_loop
                         

    xor rsi, rsi            ; tx = 0
tile_col_loop:
    cmp rsi, r9             ; tx >= tileSize
    jge inc_tile_row
    mov rax, rcx              ; x + tx >= width
    add rax, rsi
    cmp rax, r15
    jge inc_tile_row
;//////////

    ; Oblicz offset w buforze wynikowym
    mov rax, r11                  ; y
    add rax, rdi                  ; y + ty
    imul rax, r10                 ; (y + ty) * stride

    push rbx
    mov rbx, rcx                  ; x
    add rbx, rsi                  ; (x+tx)
    lea rax, [rax + rbx*4]        ; ((y + ty) * stride)+ ((x+tx) * 4)
    pop rbx

    add rax, r12                  ; ((y + ty) * stride)+ (x * 4) + sourceBuffer
                                      
    movdqu xmm3, [rax]    ; Wczytaj 4 piksele BGRA naraz

    ; Wyodrêbnia i przetwórza sk³adow¹ B
    movdqa xmm5, xmm3           ; Kopiowanie 4 pikseli do xmm1
    pand xmm5, xmmword ptr [mask_b]  ; zostawia tylko  B
    cvtdq2ps xmm5, xmm5         ; Konwertuje 4 wartoœci int na float
    addps xmm2, xmm5            ; 

    ;  przetwórz  G
    movdqa xmm6, xmm3           
    pand xmm6, xmmword ptr [mask_g]  
    psrld xmm6, 8               ; Przesuniêcie bitowe  
    cvtdq2ps xmm6, xmm6        
    addps xmm1, xmm6            

    ;  przetwórz  R
    movdqa xmm7, xmm3          
    pand xmm7, xmmword ptr [mask_r]  
    psrld xmm7, 16                ; Przesuniêcie bitowe  
    cvtdq2ps xmm7, xmm7        
    addps xmm0, xmm7          

    inc rbx                      ; pixelCount++

;//////////
inc_tile_col:
    inc rsi
    jmp tile_col_loop

inc_tile_row:
    inc rdi
    jmp tile_row_loop

      
exit_tile_loop:
;//////////////////////// KONIEC PIERWSZEJ PÊTLI////////////////

    cmp rbx, 0
    jl Exit 
    cvtsi2ss xmm3, ebx           ; Konwertuj licznik na float

    divss xmm0, xmm3             ; R average
    divss xmm1, xmm3             ; G average
    divss xmm2, xmm3             ; B average

          
    push r12      
    push r8  
    push rbp


    cvtss2si r12d, xmm0           ; R
    cvtss2si r8d, xmm1           ; G
    cvtss2si ebp, xmm2           ; B 


;//////////////////////// DRUGA PÊTLA////////////////
    xor rdi, rdi         ; ty = 0
tile_row_loop2:
    cmp rdi, r9          ; ty >= tileSize
    jge exit_tile_loop2
    mov rax, r11
    add rax, rdi
    cmp rax, r14          ; y + ty >= heigth
    jge exit_tile_loop2
                         

    xor rsi, rsi            ; tx = 0
tile_col_loop2:
    cmp rsi, r9             ; tx >= tileSize
    jge inc_tile_row2
    mov rax, rcx              ; x + tx >= width
    add rax, rsi
    cmp rax, r15
    jge inc_tile_row2
;//////////

    ; Oblicz offset w buforze wynikowym
    mov rax, r11                  ; y
    add rax, rdi                  ; y + ty
    imul rax, r10                 ; (y + ty) * stride

    push rbx
    mov rbx, rcx                  ; x
    add rbx, rsi                  ; (x+tx)
    lea rax, [rax + rbx*4]        ; ((y + ty) * stride)+ ((x+tx) * 4)
    pop rbx

    add rax, r13                  ; ((y + ty) * stride)+ (x * 4) + resultBuffer

    ; Zapisz wartoœci RGB
    mov byte ptr [rax], bpl         ; B
    mov byte ptr [rax+1], r8b       ; G  
    mov byte ptr [rax+2], r12b      ; R
    mov byte ptr [rax+3], 255       ; A

;//////////
inc_tile_col2:
    inc rsi
    jmp tile_col_loop2

inc_tile_row2:
    inc rdi
    jmp tile_row_loop2

exit_tile_loop2:    
;//////////////////////// KONIEC DRUGIEJ PÊTLI////////////////

    pop rbp
    pop r8  
    pop r12      

 
;///////////////////// 
inc_col:    
    add rcx, r9         ;x + tileSize
    jmp col_loop

inc_row:
    add r11, r9         ;y + tileSize
    jmp row_loop

Exit:
    ;mov rax, r8
    ; Przywróæ stos i rejestry
   
    pop r15
    pop r14
    pop r13
    pop r12
    pop rdi
    pop rsi
    pop rbx
    pop rbp
    ret

ApplyMosaic ENDP
END